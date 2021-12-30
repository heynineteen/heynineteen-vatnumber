namespace HeyNineteen.VatNumber
{
    using HeyNineteen.Core.Extensions;
    using System;
    using System.Linq;

    internal static class UkVatNumberCheckDigit
    {
        public static string GenerateModulos97(string input) => Generate(input);

        public static string GenerateModulos9755(string input) => Generate(input, addend: 55);

        private static string Generate(string input, int addend = 0)
        {
            _ = input ?? throw new ArgumentNullException(nameof(input));

            if(input.Length != 7 || input.ContainsNonDigit())
                throw new ArgumentException($"{nameof(input)} '{input}' must be 7 digits.", nameof(input));

            var multiplier = 8;

            var total = input.Aggregate(0, (previous, digit) => previous + (digit - '0') * multiplier--);

            total += addend;

            while (total > 0)
            {
                total -= 97;
            }

            return Math.Abs(total).ToString("00");
        }
    }
}