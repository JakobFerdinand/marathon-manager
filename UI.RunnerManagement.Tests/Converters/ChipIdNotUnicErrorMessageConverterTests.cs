using Core.Models;
using System;
using System.Collections.Generic;
using UI.RunnerManagement.Converters;
using Xunit;

namespace UI.RunnerManagement.Tests.Converters
{
    public class ChipIdNotUnicErrorMessageConverterTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            Assert.NotNull(converter);
        }
        [Fact]
        [Trait("Unit", "")]
        public void ConvertBack_any_Arguments_NotImplementedException()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack("", null, null, null));
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, new[] { GetType() }, null, null));
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_valuesOf1_true_Returns_false()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();

            var values = new object[] { true };

            var result = converter.IsErrorMessageNeeded(values);

            Assert.False(result);
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_null_NullReferenceException()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            Assert.Throws<NullReferenceException>(() => converter.IsErrorMessageNeeded(null));
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_emptyArray_IndexOutOfRangeException()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[0];
            Assert.Throws<IndexOutOfRangeException>(() => converter.IsErrorMessageNeeded(values));
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_ArrayOfSize1_withValue_false_IndexOutOfRangeException()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[] { false };
            Assert.Throws<IndexOutOfRangeException>(() => converter.IsErrorMessageNeeded(values));
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_false_and_null_returns_false()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[] { false, null };

            var result = converter.IsErrorMessageNeeded(values);

            Assert.False(result);
        }
        [Theory]
        [InlineData(5)]
        [InlineData("")]
        [InlineData("/%(§)=§/(%&)§/$§$")]
        [InlineData(true)]
        [InlineData(Math.PI)]
        [InlineData(Math.E)]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_false_and_anything_that_is_not_IenumerableOfRunner_returns_False(object value2)
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[] { false, value2 };
            var result = converter.IsErrorMessageNeeded(values);
            Assert.False(result);
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_false_and_empty_IEnumerableOfRunner_returns_False()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[] { false, new List<Runner>() };
            var result = converter.IsErrorMessageNeeded(values);
            Assert.False(result);
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_values_false_IEnumerableOfRunner_returns_true()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var runners = new List<Runner>
            {
                new Runner { }
            };
            var values = new object[] { false, runners };

            var result = converter.IsErrorMessageNeeded(values);

            Assert.True(result);
        }
        [Fact]
        [Trait("Unit", "")]
        public void IsErrorMessageNeeded_returns_false_Convert_returns_empty_string()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var values = new object[] { true };

            var result = converter.Convert(values, null, null, null);

            Assert.Equal(string.Empty, result);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Convert_values_false_IEnumerableOfRunner_returns_ErrorMessage()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            var runners = new List<Runner>
            {
                new Runner { ChipId = "0123456789"},
                new Runner { ChipId = "0123456789"}
            };
            var values = new object[] { false, runners };

            var result = converter.Convert(values, null, null, null);

            Assert.NotNull(result);
            Assert.True(result is string);
            var message = result as string;
            Assert.StartsWith("Die Chip Ids müssen eindeutig sein!", message);
            Assert.Contains("Folgende Läufer haben die gleiche Chip Id: ", message);
            Assert.Contains("0123456789", message);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Convert_values_null_NullReferenceException()
        {
            var converter = new ChipIdNotUnicErrorMessageConverter();
            Assert.Throws<NullReferenceException>(() => converter.Convert(null, null, null, null));
        }
    }
}
