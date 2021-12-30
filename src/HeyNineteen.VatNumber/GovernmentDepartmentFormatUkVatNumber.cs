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
    ///     001
    ///     GBGD001
    ///     GB888800112
    /// </summary>
    internal class GovernmentDepartmentFormatUkVatNumber : PublicSectorFormatUkVatNumber
    {
        private const string CheckDigitGroup = "checkdigit";
        private const string NumberGroup= "number";

        private static readonly string RegexString = @$"^((GB)?(GD)?(?'{NumberGroup}'[0-4][0-9]{{2}}))$";
        private static readonly string RegexStringEu = @$"^((GB)({Prefix})(?'{NumberGroup}'[0-4][0-9]{{2}})(?'{CheckDigitGroup}'[0-9]{{2}}))$";

        private static readonly Regex Regex = new(RegexString, RegexOptions.Compiled);
        private static readonly Regex RegexEu = new(RegexStringEu, RegexOptions.Compiled);

        public new static IResult<GovernmentDepartmentFormatUkVatNumber> Parse(string value)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var result = ParseNonEu(value);

            if (result.IsFailure)
                return ParseEu(value);

            return result;
        }

        private static IResult<GovernmentDepartmentFormatUkVatNumber> ParseNonEu(string value)
        {
            var vatNumberNoWhitespace = value.RemoveWhitespace();

            var match = Regex.Match(vatNumberNoWhitespace);

            if (!match.Success)
                return Result.Fail<GovernmentDepartmentFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number = match.Groups[NumberGroup].Value;

            Debug.Assert(number.Length == 3);

            var ukVatNumber = new GovernmentDepartmentFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        private static IResult<GovernmentDepartmentFormatUkVatNumber> ParseEu(string value)
        {
            var vatNumberNoWhitespace = value.RemoveWhitespace();

            var match = RegexEu.Match(vatNumberNoWhitespace);

            if (!match.Success)
                return Result.Fail<GovernmentDepartmentFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number = match.Groups[NumberGroup].Value;
            var checkDigit = match.Groups[CheckDigitGroup].Value;

            Debug.Assert(number.Length == 3);
            Debug.Assert(checkDigit.Length == 2);

            if (CheckDigitType.FromValue(Prefix + number, checkDigit) != CheckDigitType.Modulos97)
                return Result.Fail<GovernmentDepartmentFormatUkVatNumber>(
                    $"Vat number '{match.Value}' failed check digit validation for Government Department number.");

            var ukVatNumber = new GovernmentDepartmentFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        public GovernmentDepartmentFormatUkVatNumber(string value) : base(value)
        {
        }

        public override UkVatNumberType VatNumberType => UkVatNumberType.GovernmentDepartment;
    }
}