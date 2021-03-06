﻿using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstNullOrWhiteSpace
    {
        [Theory]
        [InlineData("a")]
        [InlineData("1")]
        [InlineData("some text")]
        [InlineData(" leading whitespace")]
        [InlineData("trailing whitespace ")]
        public void DoesNothingGivenNonEmptyStringValue(string nonEmptyString)
        {
            Guard.WithValue(nonEmptyString).AgainstNullOrWhiteSpace("string");
            Guard.WithValue(nonEmptyString).AgainstNullOrWhiteSpace("aNumericString");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.WithValue<string>(null!).AgainstNullOrWhiteSpace("null"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue("").AgainstNullOrWhiteSpace("emptystring"));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("   ")]
        public void ThrowsGivenWhiteSpaceString(string whiteSpaceString)
        {
            Assert.Throws<ArgumentException>(() => Guard.WithValue(whiteSpaceString).AgainstNullOrWhiteSpace("whitespacestring"));
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("1", "1")]
        [InlineData("some text", "some text")]
        [InlineData(" leading whitespace", " leading whitespace")]
        [InlineData("trailing whitespace ", "trailing whitespace ")]
        public void ReturnsExpectedValueGivenNonEmptyStringValue(string nonEmptyString, string expected)
        {
            Assert.Equal(expected, Guard.WithValue(nonEmptyString).AgainstNullOrWhiteSpace("string").Value);
            Assert.Equal(expected, Guard.WithValue(nonEmptyString).AgainstNullOrWhiteSpace("aNumericString").Value);
        }
    }
}
