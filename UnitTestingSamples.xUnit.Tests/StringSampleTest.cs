using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestingSamples.xUnit.Tests
{
    public class StringSampleTest
    {
        [Fact]
        public void ConstructorShouldThrowOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StringSample(null));
        }

        [Fact]
        public void GetStringDemoExceptions()
        {
            var sample = new StringSample(string.Empty);
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo(null, "a"));
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo("a", null));
            Assert.Throws<ArgumentException>(() => sample.GetStringDemo(string.Empty, "a"));
        }

        [Theory]
        [InlineData("", "a", "b", "b not found in a")]
        [InlineData("", "long string", "ong", "removed ong from long string: l string")]
        [InlineData("init", "abcdefghj", "f", "INIT")]
        public void GetStringDemoInlineData(string init, string a, string b, string expected)
        {
            var samle = new StringSample(init);
            string actual = samle.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetStringSampleData() => 
            new [] 
            {
                new object[] {"", "a", "b", "b not found in a" },
                new object[] {"", "long string", "ong", "removed ong from long string: l string" },
                new object[] {"init", "abcdefghj", "f", "INIT" }
            };

        [Theory]
        [MemberData(nameof(GetStringSampleData))]
        public void GetStringDemoMemberData(string init, string a, string b, string expected)
        {
            var samle = new StringSample(init);
            string actual = samle.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }
    }
}
