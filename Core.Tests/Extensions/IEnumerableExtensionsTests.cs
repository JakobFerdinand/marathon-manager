using Xunit;
using Core.Extensions;
using System.Collections.Generic;
using System;

namespace Core.Tests.Models.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void ContainsEqual_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<short>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<int>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<long>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<bool>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<byte>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<DateTime>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<TimeSpan>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<float>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<double>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<string>(null));
            Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ConaintsEqual<List<int>>(null));
        }
        [Fact]
        public void ContainsEqual_null_ArgumentNullException_AsExtension()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.ConaintsEqual());
        }

        [Fact]
        public void ConaintsEqual_intArray_3different_returns_false()
        {
            var soruce = new int[] { 4, 96, 980 };

            var result = soruce.ConaintsEqual();

            Assert.False(result);
        }

        [Fact]
        public void ConaintsEqual_intArray_2_of_3different_returns_true()
        {
            var soruce = new int[] { 96, 96, 980 };

            var result = soruce.ConaintsEqual();

            Assert.True(result);
        }

        [Fact]
        public void ConaintsEqual_intArray_3equal_returns_true()
        {
            var soruce = new int[] { 0, 0, 0 };

            var result = soruce.ConaintsEqual();

            Assert.True(result);
        }

        [Fact]
        public void ContainsEqual_emptyIntList_returns_false()
        {
            var source = new List<int>();
            source.Clear();

            var result = source.ConaintsEqual();

            Assert.False(result);
        }

        [Fact]
        public void ConaintsEqual_doubleArray_3different_returns_false()
        {
            var soruce = new double[] { 4.4, 96.0, 96.01 };

            var result = soruce.ConaintsEqual();

            Assert.False(result);
        }

        [Fact]
        public void ConaintsEqual_doubleArray_2_of_3different_returns_true()
        {
            var soruce = new double[] { 96.7843, 96.7843, 980.0 };

            var result = soruce.ConaintsEqual();

            Assert.True(result);
        }

        [Fact]
        public void ConaintsEqual_doubleArray_3equal_returns_true()
        {
            var soruce = new double[] { 1.21, 1.21, 1.21 };

            var result = soruce.ConaintsEqual();

            Assert.True(result);
        }

        [Fact]
        public void ContainsEqual_emptyDoubleList_returns_false()
        {
            var source = new List<double>();
            source.Clear();

            var result = source.ConaintsEqual();

            Assert.False(result);
        }
    }
}
