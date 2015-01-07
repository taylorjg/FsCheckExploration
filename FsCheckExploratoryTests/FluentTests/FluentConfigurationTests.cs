using FsCheck;
using FsCheckExploratoryTests.Utils;
using NUnit.Framework;
using FsCheck.Fluent;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentConfigurationTests
    {
        [Test]
        public void MaxNbOfTest()
        {
            Spec
                .ForAny((int[] ints) => ints.Length >= 0 && ints.Length < 8)
                .Check(Config.Verbose.ToConfiguration().WithName("Demo").WithMaxTest(50));
        }
    }
}
