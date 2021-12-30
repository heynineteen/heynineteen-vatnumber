namespace HeyNineteen.VatNumber
{
    using Core.Results;
    using System;
    using System.Collections.Generic;

    public abstract class UkVatNumber
    {
        protected const string CountryCode = "GB";

        private static readonly IEnumerable<Func<string, IResult<UkVatNumber>>> ParseDelegates = 
            new Func<string, IResult<UkVatNumber>>[]
            {
                GovernmentDepartmentFormatUkVatNumber.Parse,
                HealthAuthorityFormatUkVatNumber.Parse,
                StandardFormatUkVatNumber.Parse,
                BranchTradersFormatUkVatNumber.Parse,
            };
        
        public static IResult<UkVatNumber> Parse(string vatNumber)
        {
            _ = vatNumber ?? throw new ArgumentNullException(nameof(vatNumber));

            IResult<UkVatNumber> result = null;

            foreach(var parse in ParseDelegates)
            {
                result = parse(vatNumber);
                if (result.IsSuccess)
                    break;
            }

            return result;
        }

        public static IResult Validate(string vatNumber) => Parse(vatNumber);

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