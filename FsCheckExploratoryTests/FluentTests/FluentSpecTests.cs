using FsCheck;
using FsCheck.Fluent;
using FsCheckUtils;
using NUnit.Framework;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentSpecTests
    {
        private static readonly Config Config = Config.VerboseThrowOnFailure;
        private static readonly Configuration Configuration = Config.ToConfiguration();

        [Test]
        public void ForAnyInt()
        {
            Spec.ForAny((int _) => true).Check(Configuration);
        }

        [Test]
        public void ForAnyEmployee()
        {
            Spec.ForAny((Employee _) => true).Check(Configuration);
        }
    }
}
