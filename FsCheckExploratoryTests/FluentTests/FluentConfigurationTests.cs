using FsCheck;
using FsCheckUtils;
using NUnit.Framework;
using FsCheck.Fluent;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentConfigurationTests
    {
        [Test]
        public void MaxTest()
        {
            Spec
                .ForAny((int[] ints) => ints.Length >= 0 && ints.Length < 8)
                .Check(Config.Verbose.ToConfiguration().WithName("Demo").WithMaxTest(50));
        }
    }
}
