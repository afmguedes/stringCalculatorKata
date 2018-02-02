﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringCalculatorKata.Tests.StringCalculatorTests
{
    [TestFixture]
    public class AddShould
    {
        [Test]
        public void ReturnZero_WhenCalledWithEmptyString()
        {
            var expectedResult = 0;

            var actual = StringCalculator.Add(string.Empty);

            actual.Should().Be(expectedResult);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("1000000000", 1000000000)]
        public void ReturnExpectedResult_WhenCalledWithStringWithOneNumber(string oneNumber, int expectedResult)
        {
            var actual = StringCalculator.Add(oneNumber);

            actual.Should().Be(expectedResult);
        }

        [TestCase("1,2", 3)]
        [TestCase("5, 10", 15)]
        [TestCase("2,3", 5)]
        public void ReturnExpectedResult_WhenCalledWithStringWithTwoNumbers(string twoNumbers, int expectedResult)
        {
            var actual = StringCalculator.Add(twoNumbers);

            actual.Should().Be(expectedResult);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4", 10)]
        [TestCase("1,2,3,4,5", 15)]
        public void ReturnExpectedResult_WhenCalledWithUnknownAmountOfNumbers(string unknownAmountOfNumbers,
            int expectedResult)
        {
            var actual = StringCalculator.Add(unknownAmountOfNumbers);

            actual.Should().Be(expectedResult);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1,2\n3", 6)]
        [TestCase("1\n2,3\n4,5", 15)]
        public void ReturnExpectedResult_WhenCalledWithNumbersWithDifferentSeparators(
            string numbersWithDifferentSeparators, int expectedResult)
        {
            var actual = StringCalculator.Add(numbersWithDifferentSeparators);

            actual.Should().Be(expectedResult);
        }

        [TestCase("//;\n4;2", 6)]
        [TestCase("//.\n1.2.3", 6)]
        [TestCase("//#\n1#2#3#4", 10)]
        public void ReturnExpectedResult_WhenCalledWithNumbersWithCustomSeparators(string numbersWithCustomSeparators,
            int expectedResult)
        {
            var actual = StringCalculator.Add(numbersWithCustomSeparators);

            actual.Should().Be(expectedResult);
        }

        [TestCase("-1", "Negatives not allowed. -1")]
        [TestCase("-1,1", "Negatives not allowed. -1")]
        [TestCase("-2,-1", "Negatives not allowed. -2,-1")]
        [TestCase("//;\n-1", "Negatives not allowed. -1")]
        [TestCase("//;\n-2;-1", "Negatives not allowed. -2,-1")]
        [TestCase("//;\n-2;-1;1", "Negatives not allowed. -2,-1")]
        public void ThrowExceptionWithExpectedMessage_WhenCalledWithNegativeNumbers(string negativeNumbers, string expectedMessage)
        {
            Action action = () => StringCalculator.Add(negativeNumbers);

            action.ShouldThrow<ArgumentException>().WithMessage(expectedMessage);
        }
    }
}