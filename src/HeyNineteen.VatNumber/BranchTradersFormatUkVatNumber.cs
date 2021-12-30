namespace HeyNineteen.VatNumber
{
    using System;
    using Core.Results;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using Core.Extensions;

    /// <summary>
    /// https://en.wikipedia.org/wiki/VAT_identification_number
    /// 
    /// Possible formats:
    ///
    ///     123456789123
    ///     GB123456789123
    /// </summary>
    internal class BranchTradersFormatUkVatNumber : UkVatNumber
    {
        private const string CheckDigitGroup = "checkdigit";
        private const string Number1Group = "number1";
        private const string Number2Group = "number2";

        private static readonly string RegexString = @$"((GB)?(?'{Number1Group}'[1-9][0-9]{{6}})(?'{CheckDigitGroup}'[0-9]{{2}})(?'{Number2Group}'[0-9]{{3}}))$";

        private static readonly Regex Regex = new(RegexString, RegexOptions.Compiled);

        public new static IResult<BranchTradersFormatUkVatNumber> Parse(string value)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var valueNoWhitespace = value.RemoveWhitespace();

            var match = Regex.Match(valueNoWhitespace);

            if (!match.Success)
                return Result.Fail<BranchTradersFormatUkVatNumber>($"{nameof(value)} is an invalid format: '{value}'");

            var number1 = match.Groups[Number1Group].Value;
            var number2 = match.Groups[Number2Group].Value;
            var checkDigit = match.Groups[CheckDigitGroup].Value;

            Debug.Assert(number1.Length == 7);
            Debug.Assert(number2.Length == 3);
            Debug.Assert(checkDigit.Length == 2);

            var checkDigitType = CheckDigitType.FromValue(number1, checkDigit);

            if (checkDigitType == null)
                return Result.Fail<BranchTradersFormatUkVatNumber>(
                    $"Vat number '{match.Value}' failed check digit validation for branch traders number.");

            var ukVatNumber = new BranchTradersFormatUkVatNumber(
                number1.Substring(0, 3),
                number1.Substring(3),
                checkDigit,
                checkDigitType,
                number2);

            return Result.Ok(ukVatNumber);
        }

        private BranchTradersFormatUkVatNumber(
            string block1,
            string block2,
            string block3,
            CheckDigitType checkDigitType,
            string block4
            )
            : base(block1, block2, block3, checkDigitType, block4)
        {
        }

        public override string LongFormat => $"{CountryCode}{Block1}{Block2}{Block3}{Block4}";

        public override string ShortFormat => $"{Block1}{Block2}{Block3}{Block4}";

        public override string EuCompatibleFormat => LongFormat;

        public override UkVatNumberType VatNumberType => UkVatNumberType.BranchTrader;
    }
}