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
            var upperLimit = 1000;

            if (string.IsNullOrEmpty(numbers))
                return zero;

            var arrayOfIntegers = SplitCsv(numbers);

            ValidateNumbersOrThrow(arrayOfIntegers);

            return arrayOfIntegers.Where(number => number <= upperLimit).Sum();
        }

        private static int[] SplitCsv(string numbers)
        {
            int[] arrayOfIntegers;
            var customDelimiterFlag = "//";
            var pattern = @"\/\/(.)\n((-*\d+)\1*)+";
            var defaultDelimiters = new[] {',', '\n'};

            if (numbers.StartsWith(customDelimiterFlag))
                arrayOfIntegers = SplitCsvWithCustomDelimiters(numbers, pattern);
            else
                arrayOfIntegers = SplitCsvWithDefaultDelimiters(numbers, defaultDelimiters);

            return arrayOfIntegers;
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