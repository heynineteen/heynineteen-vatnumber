namespace HeyNineteen.VatNumber
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    public class UkVatNumberType
    {
        public static readonly UkVatNumberType Standard = new("Standard");
        public static readonly UkVatNumberType BranchTrader = new("BranchTrader");
        public static readonly UkVatNumberType GovernmentDepartment = new("GovernmentDepartment", "GD");
        public static readonly UkVatNumberType HealthAuthority = new("HealthAuthority", "HA");

        private static readonly List<UkVatNumberType> All = new() { Standard, BranchTrader, GovernmentDepartment, HealthAuthority };

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