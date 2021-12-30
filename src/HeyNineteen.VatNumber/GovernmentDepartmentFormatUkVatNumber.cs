namespace HeyNineteen.VatNumber
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using HeyNineteen.Core.Results;

    internal class GovernmentDepartmentFormatUkVatNumber : PublicSectorFormatUkVatNumber
    {
        public static IResult<GovernmentDepartmentFormatUkVatNumber> Create(Match match)
        {
            var number = match.Groups[UkVatNumberRegex.GovernmentDepartmentNumber].Value;

            Debug.Assert(number.Length == 3);

            var ukVatNumber = new GovernmentDepartmentFormatUkVatNumber(number);

            return Result.Ok(ukVatNumber);
        }

        public static IResult<GovernmentDepartmentFormatUkVatNumber> CreateFromEuFormat(Match match)
        {
            var number = match.Groups[UkVatNumberRegex.GovernmentDepartmentEuNumber].Value;
            var checkDigit = match.Groups[UkVatNumberRegex.GovernmentDepartmentEuCheckDigit].Value;

            Debug.Assert(number.Length == 3);
            Debug.Assert(checkDigit.Length == 2);

            if (CheckDigitType.FromValue(UkVatNumberRegex.EuNumberPrefix + number, checkDigit) != CheckDigitType.Modulos97)
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