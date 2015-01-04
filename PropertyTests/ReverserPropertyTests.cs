using FsCheck.Fluent;
using NUnit.Framework;
using CaseStudy;

namespace CaseStudyPropertyTests
{
    [TestFixture]
    internal class ReverserPropertyTests
    {
        [FsCheck.NUnit.Property]
        public void TestUsingFsCheck2()
        {
            Spec
                .ForAny<string>(s => s.Reverse().Reverse() == s)
                .QuickCheckThrowOnFailure();
        }
    }
}
