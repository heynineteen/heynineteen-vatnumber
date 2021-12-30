namespace HeyNineteen.VatNumber
{
    internal abstract class PublicSectorFormatUkVatNumber : UkVatNumber
    {
        protected const string Prefix = "8888";

        protected PublicSectorFormatUkVatNumber(string value)
            : base(value, null, UkVatNumberCheckDigit.GenerateModulos97(Prefix + value), CheckDigitType.Modulos97)
        {
        }

        public override string LongFormat => $"{CountryCode}{VatNumberType.Code}{Block1}";

        public override string ShortFormat => $"{VatNumberType.Code}{Block1}";

        public override string EuCompatibleFormat => $"{CountryCode}{Prefix}{Block1}{Block3}";
    }
}