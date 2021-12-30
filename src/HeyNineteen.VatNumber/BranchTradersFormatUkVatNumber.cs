namespace HeyNineteen.VatNumber
{
    using HeyNineteen.Core.Results;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    internal class BranchTradersFormatUkVatNumber : UkVatNumber
    {
        public static IResult<BranchTradersFormatUkVatNumber> Create(Match match)
        {
            var number1 = match.Groups[UkVatNumberRegex.BranchTradersNumber1].Value;
            var number2 = match.Groups[UkVatNumberRegex.BranchTradersNumber2].Value;
            var checkDigit = match.Groups[UkVatNumberRegex.BranchTradersCheckDigit].Value;

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