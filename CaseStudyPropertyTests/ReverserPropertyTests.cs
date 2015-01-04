using FsCheck.Fluent;
using NUnit.Framework;
using CaseStudy;

namespace CaseStudyPropertyTests
{
    [TestFixture]
    internal class ReverserPropertyTests
    {
        [FsCheck.NUnit.Property]
        public void StringReversedAndThenReversedAgainIsSameAsOriginalString()
        {
            Spec
                .ForAny<string>(s => s.Reverse().Reverse() == s)
                .QuickCheckThrowOnFailure();
        }
    }
}
