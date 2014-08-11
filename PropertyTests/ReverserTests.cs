using Code;
using FsCheck.Fluent;
using NUnit.Framework;

namespace PropertyTests
{
    [TestFixture]
    internal class ReverserTests
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

        [FsCheck.NUnit.Property]
        public void TestUsingFsCheck()
        {
            var gen = Any.OfType<string>();
            Spec
                .For(gen, s => s.Reverse().Reverse() == s)
                .QuickCheckThrowOnFailure();
        }
    }
}
