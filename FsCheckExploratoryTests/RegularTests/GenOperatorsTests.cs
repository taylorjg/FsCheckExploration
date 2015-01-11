using FsCheck;
using FsCheckExploratoryTests.Utils;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests.RegularTests
{
    [TestFixture]
    internal class GenOperatorsTests
    {
        [Test, Description("<!>")]
        public void OpLessBangGreater()
        {
            var genPositiveInt = Arb.Default.PositiveInt().Generator;
            var func = FSharpFunc<PositiveInt, string>.FromConverter(pi => new string('X', pi.Get));
            var genString = GenOperators.op_LessBangGreater(func, genPositiveInt);
            genString.DumpSamples();
        }

        [Test, Description("<*>")]
        public void OpLessMultiplyGreater()
        {
            var genPositiveInt = Arb.Default.PositiveInt().Generator;
            var func = FSharpFunc<PositiveInt, string>.FromConverter(pi => new string('X', pi.Get));
            var genFunc = Gen.constant(func);
            var genString = GenOperators.op_LessMultiplyGreater(genFunc, genPositiveInt);
            genString.DumpSamples();
        }
    }
}
