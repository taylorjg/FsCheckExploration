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
            var shrinker = Shrink.Type<int>();
        }
    }
}
