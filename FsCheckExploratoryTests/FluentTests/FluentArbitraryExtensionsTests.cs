using FsCheck;
using FsCheck.Fluent;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentArbitraryExtensionsTests
    {
        private static bool PropFred(int? ni)
        {
            return ni.HasValue;
        }

        [Test, Description("Same as Arb.convert")]
        public void Convert()
        {
            var arbInt = Arb.Default.Int32();
            var arbNullableInt = arbInt.Convert(i => new int?(i), ni => ni.Value);
            Check.VerboseThrowOnFailure(Prop.forAll(arbNullableInt, FSharpFunc<int?, bool>.FromConverter(PropFred)));

            // See the following for an idea for a better example of using ArbitraryExtensions.Convert:
            // http://stackoverflow.com/questions/21988046/how-does-one-generate-a-complex-object-in-fscheck
            // When do convertFrom and convertTo get called ? check the FsCheck source code.
        }
    }
}
