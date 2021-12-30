namespace HeyNineteen.VatNumber
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// https://en.wikipedia.org/wiki/VAT_identification_number
    /// 
    /// Possible formats:
    ///
    /// Standard
    ///     123456789
    ///     GB123456789
    ///
    /// Government Department
    ///     001
    ///     GBGD001
    ///
    /// Health Authority
    ///     500
    ///     GBHA500
    ///
    /// Government Department EU
    ///     GB888800112
    ///
    /// Health Authority EU
    ///     GB888850012
    ///
    /// Branch Trader
    ///     123456789123
    ///     GB123456789123
    /// </summary>
    internal static class UkVatNumberRegex
    {
        public const string GovernmentDepartment= "govdept";
        public const string HealthAuthority = "healthauth";
        public const string GovernmentDepartmentEu = "govdepteu";
        public const string HealthAuthorityEu = "healthautheu";
        public const string Standard = "standard";
        public const string BranchTraders = "branchtraders";

        public const string GovernmentDepartmentEuCheckDigit = "govdepteucheck";
        public const string HealthAuthorityEuCheckDigit = "healthautheucheck";
        public const string StandardCheckDigit = "scheck";
        public const string BranchTradersCheckDigit = "btcheck";

        public const string GovernmentDepartmentNumber = "gdnumber";
        public const string HealthAuthorityNumber = "hanumber";
        public const string GovernmentDepartmentEuNumber = "gdeunumber";
        public const string HealthAuthorityEuNumber = "haeunumber";
        public const string EuNumberPrefix = "8888";
        public const string StandardNumber = "snumber";
        public const string BranchTradersNumber1 = "btnumber1";
        public const string BranchTradersNumber2 = "btnumber2";

        private static readonly string RegexString = 
            @$"^(?'{GovernmentDepartment}'(GB)?(GD)?(?'{GovernmentDepartmentNumber}'[0-4][0-9]{{2}}))$" +
            $"|^(?'{GovernmentDepartmentEu}'(GB)({EuNumberPrefix})(?'{GovernmentDepartmentEuNumber}'[0-4][0-9]{{2}})(?'{GovernmentDepartmentEuCheckDigit}'[0-9]{{2}}))$" +
            $"|^(?'{HealthAuthority}'(GB)?(HA)?(?'{HealthAuthorityNumber}'[5-9][0-9]{{2}}))$" +
            $"|^(?'{HealthAuthorityEu}'(GB)({EuNumberPrefix})(?'{HealthAuthorityEuNumber}'[5-9][0-9]{{2}})(?'{HealthAuthorityEuCheckDigit}'[0-9]{{2}}))$" +
            $"|^(?'{Standard}'(GB)?(?'{StandardNumber}'[1-9][0-9]{{6}})(?'{StandardCheckDigit}'[0-9]{{2}}))$" +
            $"|(?'{BranchTraders}'(GB)?(?'{BranchTradersNumber1}'[1-9][0-9]{{6}})(?'{BranchTradersCheckDigit}'[0-9]{{2}})(?'{BranchTradersNumber2}'[0-9]{{3}}))$";

        public static Regex Regex = new(RegexString, RegexOptions.Compiled);
    }
}