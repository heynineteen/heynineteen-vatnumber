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
    ///     123456789
    ///     GB123456789
    /// </summary>
    internal class StandardFormatUkVatNumber : UkVatNumber
    {
        private const string CheckDigitGroup = "checkdigit";
        private const string NumberGroup = "number";
     
        private static readonly string RegexString = @$"^((GB)?(?'{NumberGroup}'[1-9][0-9]{{6}})(?'{CheckDigitGroup}'[0-9]{{2}}))$";

        private static readonly Regex Regex = new(RegexString, RegexOptions.Compiled);

        public new static IResult<StandardFormatUkVatNumber> Parse(string value)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var valueNoWhitespace = value.RemoveWhitespace();

            var match = Regex.Match(valueNoWhitespace);

            if (!match.Success)
                return Result.Fail<StandardFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number = match.Groups[NumberGroup].Value;
            var checkDigit = match.Groups[CheckDigitGroup].Value;

            Debug.Assert(number.Length == 7);
            Debug.Assert(checkDigit.Length == 2);

            var checkDigitType = CheckDigitType.FromValue(number, checkDigit);

            if (checkDigitType == null)
                return Result.Fail<StandardFormatUkVatNumber>(
                    $"Vat number '{match.Value}' failed check digit validation for standard number.");

            var ukVatNumber = new StandardFormatUkVatNumber(
                number.Substring(0, 3),
                number.Substring(3),
                checkDigit,
                checkDigitType);

            return Result.Ok(ukVatNumber);
        }

        private StandardFormatUkVatNumber(string block1, string block2, string block3,
            CheckDigitType checkDigitType)
            : base(block1, block2, block3, checkDigitType)
        {
        }

        public override string LongFormat => $"{CountryCode}{Block1}{Block2}{Block3}";

        public override string ShortFormat => $"{Block1}{Block2}{Block3}";
        
        public override string EuCompatibleFormat => LongFormat;
        
        public override UkVatNumberType VatNumberType => UkVatNumberType.Standard;
    }
}