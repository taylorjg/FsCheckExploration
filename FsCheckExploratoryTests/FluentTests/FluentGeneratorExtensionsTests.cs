using FsCheck;
using FsCheckExploratoryTests.Utils;
using NUnit.Framework;
using FsCheck.Fluent;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentGeneratorExtensionsTests
    {
        [Test, Description("Same as Gen.listOf")]
        public void MakeList()
        {
            var genInt = Any.OfType<int>();
            var gen = genInt.MakeList();
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Gen.listOfLength")]
        public void MakeListOfLength()
        {
            var genInt = Any.OfType<int>();
            var gen = genInt.MakeListOfLength(10);
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Gen.nonEmptyListOf")]
        public void MakeNonEmptyList()
        {
            var genInt = Any.OfType<int>();
            var gen = genInt.MakeNonEmptyList();
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as combination of sized and resize")]
        public void Resize()
        {
            var genIntArray = Any.OfType<int[]>();
            var gen = genIntArray.Resize(i => i * 2);
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Gen.map")]
        public void Select()
        {
            var genPositiveInt = Arb.Default.PositiveInt().Generator;
            var gen = genPositiveInt.Select(pi => new string('X', pi.Item));
            gen.DumpSamples();
        }

        [Test, Description("Same as ?")]
        public void SelectMany()
        {
        }

        [Test, Description("Same as ?")]
        public void ToArbitrary()
        {
        }

        [Test, Description("Same as Gen.suchThat")]
        public void Where()
        {
            var genInt = Any.OfType<int>();
            var gen = genInt.Where(i => i > 10 && i < 20);
            gen.DumpSamples();
        }
    }
}
