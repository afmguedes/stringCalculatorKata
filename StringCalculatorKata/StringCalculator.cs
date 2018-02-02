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
            var customDelimiterFlag = "//";
            var pattern = @"\/\/(.)\n((-*\d+)\1*)*";
            var arrayOfIntegers = new int[]{};
            var defaultDelimiters = new[] { ',', '\n' };

            if (string.IsNullOrEmpty(numbers))
                return zero;

            if (numbers.StartsWith(customDelimiterFlag))
                arrayOfIntegers = SplitCsvWithCustomDelimiters(numbers, pattern);
            else
                arrayOfIntegers = SplitCsvWithDefaultDelimiters(numbers, defaultDelimiters);

            ValidateNumbersOrThrow(arrayOfIntegers);

            return arrayOfIntegers.Sum();
        }

        private static int[] SplitCsvWithCustomDelimiters(string numbers, string pattern)
        {
            var numbersSplit = from capture
                    in Regex.Matches(numbers, pattern)[0].Groups[3].Captures.Cast<Capture>()
                select capture.Value;

            return numbersSplit.Select(int.Parse).ToArray();
        }

        private static int[] SplitCsvWithDefaultDelimiters(string numbers, char[] defaultDelimiters)
        {
            return numbers.Split(defaultDelimiters).Select(int.Parse).ToArray();
        }

        private static void ValidateNumbersOrThrow(int[] arrayOfIntegers)
        {
            var negativeNumbers = arrayOfIntegers.Where(number => number < 0).ToArray();

            if (negativeNumbers.Any())
                throw new ArgumentException(
                    $"Negatives not allowed. {string.Join(",", negativeNumbers.Select(number => number.ToString()))}");
        }
    }
}