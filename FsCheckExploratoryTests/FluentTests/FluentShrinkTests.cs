using NUnit.Framework;
using FsCheck.Fluent;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentShrinkTests
    {
        [Test, Description("Same as Arb.from<>.Shrinker")]
        public void TypeOfT()
        {
            // return Arb.Arbitrary.contents.InstanceFor<a, Arbitrary<a>>((FSharpOption<IEnumerable<object>>) null).Shrinker(a);
            var shrinker = Shrink.Type<int>();
        }
    }
}
