namespace HeyNineteen.VatNumber
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using Core.Extensions;
    using Core.Results;

    /// <summary>
    /// https://en.wikipedia.org/wiki/VAT_identification_number
    /// 
    /// Possible formats:
    ///
    ///     500
    ///     GBHA500
    ///     GB888850012
    /// </summary>
    internal class HealthAuthorityFormatUkVatNumber : PublicSectorFormatUkVatNumber
    {
        private const string CheckDigitGroup = "checkdigit";
        private const string NumberGroup = "number";

        private static readonly string RegexString = @$"^((GB)?(HA)?(?'{NumberGroup}'[5-9][0-9]{{2}}))$";
        private static readonly string RegexStringEu = @$"^((GB)({Prefix})(?'{NumberGroup}'[5-9][0-9]{{2}})(?'{CheckDigitGroup}'[0-9]{{2}}))$";

        private static readonly Regex Regex = new(RegexString, RegexOptions.Compiled);
        private static readonly Regex RegexEu = new(RegexStringEu, RegexOptions.Compiled);

        public new static IResult<HealthAuthorityFormatUkVatNumber> Parse(string value)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var result = ParseNonEu(value);

            if (result.IsFailure)
                return ParseEu(value);

            return result;
        }

        private static IResult<HealthAuthorityFormatUkVatNumber> ParseNonEu(string value)
        {
            var vatNumberNoWhitespace = value.RemoveWhitespace();

            var match = Regex.Match(vatNumberNoWhitespace);

            if (!match.Success)
                return Result.Fail<HealthAuthorityFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number = match.Groups[NumberGroup].Value;

            Debug.Assert(number.Length == 3);

            var ukVatNumber = new HealthAuthorityFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        private static IResult<HealthAuthorityFormatUkVatNumber> ParseEu(string value)
        {
            var vatNumberNoWhitespace = value.RemoveWhitespace();

            var match = RegexEu.Match(vatNumberNoWhitespace);

            if (!match.Success)
                return Result.Fail<HealthAuthorityFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number = match.Groups[NumberGroup].Value;
            var checkDigit = match.Groups[CheckDigitGroup].Value;

            Debug.Assert(number.Length == 3);
            Debug.Assert(checkDigit.Length == 2);

            if (CheckDigitType.FromValue(Prefix + number, checkDigit) != CheckDigitType.Modulos97)
                return Result.Fail<HealthAuthorityFormatUkVatNumber>(
                    $"Vat number '{match.Value}' failed check digit validation for Health Authority number.");

            var ukVatNumber = new HealthAuthorityFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        public HealthAuthorityFormatUkVatNumber(string value) : base(value)
        {
        }

        public override UkVatNumberType VatNumberType => UkVatNumberType.HealthAuthority;
    }
}