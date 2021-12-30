namespace HeyNineteen.VatNumber
{
    using Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("{Name}")]
    public class CheckDigitType
    {
        private static readonly Func<string, string> Modulos97Delegate = UkVatNumberCheckDigit.GenerateModulos97;
        private static readonly Func<string, string> Modulos9755Delegate = UkVatNumberCheckDigit.GenerateModulos9755;

        public static readonly CheckDigitType Modulos97 = new("Modulos97", Modulos97Delegate);
        public static readonly CheckDigitType Modulos9755 = new("Modulos9755", Modulos9755Delegate);

        private static readonly List<CheckDigitType> All = new() {Modulos97, Modulos9755};

        private CheckDigitType(string name, Func<string, string> checkDigitGenerator)
        {
            Name = name;
            CheckDigitGenerator = checkDigitGenerator;
        }

        public string Name { get; }

        private Func<string, string> CheckDigitGenerator { get; }

        public static CheckDigitType FromValue(string number, string candidateCheckDigit)
        {
            _ = number ?? throw new ArgumentNullException(nameof(number));
            _ = candidateCheckDigit ?? throw new ArgumentNullException(nameof(candidateCheckDigit));

            if (number.Length != 7 || number.ContainsNonDigit())
                throw new ArgumentException($"{nameof(number)} '{number}' must be 7 digits.", nameof(number));

            return All.FirstOrDefault(type => type.CheckDigitGenerator(number) == candidateCheckDigit);
        }

        public override string ToString() => Name;
    }
}