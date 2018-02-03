using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        public static int Add(string numbers)
        {
            var stringEmptyResult = 0;
            var upperLimit = 1000;

            if (string.IsNullOrEmpty(numbers))
                return stringEmptyResult;

            var arrayOfIntegers = SplitCsv(numbers);

            ValidateNumbersOrThrow(arrayOfIntegers);

            return arrayOfIntegers.Sum(IsNumberLessThan(upperLimit));
        }

        private static Func<int, int> IsNumberLessThan(int upperLimit)
        {
            return number => number <= upperLimit ? number : 0;
        }

        private static int[] SplitCsv(string numbers)
        {
            var delimiters = new[] { ",", "\n" };
            var singleDelimPattern = @"\/\/([^\[\]]+)\n";
            var multipleDelimsPattern = @"\/\/(?:\[([^\[\]]+)\])+\n";

            if (IsMatch(numbers, singleDelimPattern))
            {
                delimiters = new[] { Regex.Matches(numbers, singleDelimPattern)[0].Groups[1].Value };
                numbers = Regex.Replace(numbers, singleDelimPattern, "");
            }
            else if (IsMatch(numbers, multipleDelimsPattern))
            {
                delimiters = (from capture
                        in Regex.Matches(numbers, multipleDelimsPattern)[0].Groups[1].Captures.Cast<Capture>()
                    select capture.Value).ToArray();
                numbers = Regex.Replace(numbers, multipleDelimsPattern, "");
            }

            return numbers.Split(delimiters, StringSplitOptions.None).Select(int.Parse).ToArray();
        }

        private static bool IsMatch(string numbers, string singleDelimPattern)
        {
            return Regex.Match(numbers, singleDelimPattern).Success;
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