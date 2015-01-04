using NUnit.Framework;
using CaseStudy;

namespace CaseStudyUnitTests
{
    [TestFixture]
    internal class ReverserPropertyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        [TestCase("abcd")]
        [TestCase("abcde")]
        public void StringReversedAndThenReversedAgainIsSameAsOriginalString(string s)
        {
            Assert.That(s.Reverse().Reverse(), Is.EqualTo(s));
        }
    }
}
