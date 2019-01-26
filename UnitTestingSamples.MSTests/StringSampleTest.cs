using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestingSamples.MSTests
{
    [TestClass]
    public class StringSampleTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorShouldThrowOnNull()
        {
            var sample = new StringSample(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringDemoThrowOnFirstNull()
        {
            var sample = new StringSample(String.Empty);
            sample.GetStringDemo(null, "second");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringDemoThrowOnSecondNull()
        {
            var sample = new StringSample(String.Empty);
            sample.GetStringDemo("first", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringDemoThrowOnFirstShorterThanSecond()
        {
            var sample = new StringSample(String.Empty);
            sample.GetStringDemo("abc", "abcd");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringDemoThrowOnFirstIsEmpty()
        {
            var sample = new StringSample(String.Empty);
            sample.GetStringDemo("", "abcd");
        }

        [TestMethod]
        public void GetStringDemoOnIndexBiggerThanFive()
        {
            var expected = "INIT";
            var sample = new StringSample("InIt");
            var actual = sample.GetStringDemo("abcdefgh", "f");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringDemoBNotInA()
        {
            var expected = "b not found in a";
            var sample = new StringSample(String.Empty);
            var actual = sample.GetStringDemo("a", "b");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringDemoRemoveBCFromABCD()
        {
            var expected = "removed bc from abcd: ad";
            var sample = new StringSample(String.Empty);
            var actual = sample.GetStringDemo("abcd", "bc");
            Assert.AreEqual(expected, actual);
        }
    }
}
