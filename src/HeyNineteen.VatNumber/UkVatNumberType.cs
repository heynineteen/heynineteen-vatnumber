namespace HeyNineteen.VatNumber
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    public class UkVatNumberType
    {
        public static readonly UkVatNumberType Standard = new("Standard");
        public static readonly UkVatNumberType BranchTrader = new("BranchTrader");
        public static readonly UkVatNumberType GovernmentDepartment = new("GovernmentDepartment", "GD");
        public static readonly UkVatNumberType HealthAuthority = new("HealthAuthority", "HA");

        private UkVatNumberType(string name, string code = null)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; }

        public string Code { get; }

        public override string ToString() => Name;
    }
}