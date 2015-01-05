using FsCheck;
using FsCheck.NUnit;
using Microsoft.FSharp.Core;
using CaseStudy;

namespace CaseStudyPropertyTestsCs
{
    using PropFunc = FSharpFunc<string, bool>;

    internal class ReverserPropertyTests
    {
        private static readonly Config MyConfig = Config.VerboseThrowOnFailure;

        [Property]
        public void StringReversedAndThenReversedAgainIsSameAsOriginalString()
        {
            Check.One(MyConfig, PropFunc.FromConverter(s => s.Reverse().Reverse() == s));
        }
    }
}
