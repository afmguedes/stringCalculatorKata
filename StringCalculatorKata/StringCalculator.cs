using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        public static int Add(string numbers)
        {
            var zero = 0;

            if (numbers.StartsWith("//"))
            {
                var pattern = @"\/\/(.)\n(([0-9]+)\1*)*";

                var numbersSplit = from capture
                        in Regex.Matches(numbers, pattern)[0].Groups[3].Captures.Cast<Capture>()
                    select capture.Value;

                return numbersSplit.Sum(Convert.ToInt32);
            }

            if (numbers.Equals(string.Empty))
                return zero;

            var comaSeparator = ',';
            var newLineSeparator = '\n';

            var arrayOfNumbers = numbers.Split(comaSeparator, newLineSeparator);

            return arrayOfNumbers.Sum(Convert.ToInt32);
        }
    }
}