namespace HeyNineteen.VatNumber
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using HeyNineteen.Core.Results;

    internal class StandardFormatUkVatNumber : UkVatNumber
    {
        public static IResult<StandardFormatUkVatNumber> Create(Match match)
        {
            var number = match.Groups[UkVatNumberRegex.StandardNumber].Value;
            var checkDigit = match.Groups[UkVatNumberRegex.StandardCheckDigit].Value;

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