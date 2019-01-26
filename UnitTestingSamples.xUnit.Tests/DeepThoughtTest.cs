using System;
using Xunit;

namespace UnitTestingSamples.xUnit.Tests
{
    public class DeepThoughtTest
    {
        [Fact]
        public void ResultOfTheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything()
        {
            int expected = 42;
            var dt = new DeepThought();

            int actual = dt.TheAnswerOfTheUltimateQuestionOfLifeTheUniverseAndEverything();

            Assert.Equal(expected, actual);
        }
    }
}
