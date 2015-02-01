using CaseStudy;
using FsCheck;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace CaseStudyPropertyTestsCs
{
    using PropFunc = FSharpFunc<string, bool>;

    [TestFixture]
    public class ReverserPropertyTests
    {
        private static readonly Config MyConfig = Config.VerboseThrowOnFailure;

        [Test]
        public void StringReversedAndThenReversedAgainIsSameAsOriginalString()
        {
            Check.One(MyConfig, PropFunc.FromConverter(s => s.Reverse().Reverse() == s));
        }
    }
}
