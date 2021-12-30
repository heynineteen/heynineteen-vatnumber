namespace HeyNineteen.VatNumber
{
    using HeyNineteen.Core.Extensions;
    using HeyNineteen.Core.Results;
    using System;

    public abstract class UkVatNumber
    {
        protected const string CountryCode = "GB";

        public static IResult<UkVatNumber> Create(string vatNumber)
        {
            _ = vatNumber ?? throw new ArgumentNullException(nameof(vatNumber));

            var vatNumberNoWhitespace = vatNumber.RemoveWhitespace();

            var match = UkVatNumberRegex.Regex.Match(vatNumberNoWhitespace);

            if (!match.Success)
                return Result.Fail<UkVatNumber>($"{nameof(vatNumber)} is an invalid format: '{vatNumber}'");

            var groups = match.Groups;

            if (groups[UkVatNumberRegex.Standard].Success)
                return StandardFormatUkVatNumber.Create(match);

            if (groups[UkVatNumberRegex.GovernmentDepartment].Success)
                return GovernmentDepartmentFormatUkVatNumber.Create(match);

            if (groups[UkVatNumberRegex.HealthAuthority].Success)
                return HealthAuthorityFormatUkVatNumber.Create(match);

            if (groups[UkVatNumberRegex.GovernmentDepartmentEu].Success)
                return GovernmentDepartmentFormatUkVatNumber.CreateFromEuFormat(match);

            if (groups[UkVatNumberRegex.HealthAuthorityEu].Success)
                return HealthAuthorityFormatUkVatNumber.CreateFromEuFormat(match);

            if (groups[UkVatNumberRegex.BranchTraders].Success)
                return BranchTradersFormatUkVatNumber.Create(match);

            throw new InvalidOperationException(
                $"{nameof(vatNumber)} '{vatNumber}' was matched but a handler was not found for the {nameof(UkVatNumberType)}");
        }

        public static IResult Validate(string vatNumber) => Create(vatNumber);

        protected UkVatNumber(string block1, string block2, string block3, CheckDigitType checkDigitType, string block4 = null)
        {
            Block1 = block1;
            Block2 = block2;
            Block3 = block3;
            CheckDigitType = checkDigitType;
            Block4 = block4;
        }

        protected string Block1 { get; }

        protected string Block2 { get; }

        protected string Block3 { get; }

        protected string Block4 { get; }
        
        public CheckDigitType CheckDigitType { get; }

        public abstract string LongFormat { get; }

        public abstract string ShortFormat { get; }

        public abstract string EuCompatibleFormat { get; }

        public abstract UkVatNumberType VatNumberType { get; }
    }
}