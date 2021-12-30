namespace HeyNineteen.VatNumber
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using HeyNineteen.Core.Results;

    internal class HealthAuthorityFormatUkVatNumber : PublicSectorFormatUkVatNumber
    {
        public static IResult<HealthAuthorityFormatUkVatNumber> Create(Match match)
        {
            var number = match.Groups[UkVatNumberRegex.HealthAuthorityNumber].Value;

            Debug.Assert(number.Length == 3);

            var ukVatNumber = new HealthAuthorityFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        public static IResult<HealthAuthorityFormatUkVatNumber> CreateFromEuFormat(Match match)
        {
            var number = match.Groups[UkVatNumberRegex.HealthAuthorityEuNumber].Value;
            var checkDigit = match.Groups[UkVatNumberRegex.HealthAuthorityEuCheckDigit].Value;

            Debug.Assert(number.Length == 3);
            Debug.Assert(checkDigit.Length == 2);

            if (CheckDigitType.FromValue(UkVatNumberRegex.EuNumberPrefix + number, checkDigit) != CheckDigitType.Modulos97)
                return Result.Fail<HealthAuthorityFormatUkVatNumber>(
                    $"Vat number '{match.Value}' failed check digit validation for Government Department number.");

            var ukVatNumber = new HealthAuthorityFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        public HealthAuthorityFormatUkVatNumber(string value) : base(value)
        {
        }

        public override UkVatNumberType VatNumberType => UkVatNumberType.HealthAuthority;
    }
}