using Microsoft.FSharp.Core;
using NUnit.Framework;
using FsCheck;

namespace FsCheckExploratoryTests.RegularTests
{
    [TestFixture]
    internal class ArbTests
    {
        [Test]
        public void Convert()
        {
        }

        [Test]
        public void Filter()
        {
            var arbInt = Arb.Default.Int32();
            var arbFiltered = Arb.filter(FSharpFunc<int, bool>.FromConverter(i => i > 10 && i < 20), arbInt);
            Check.VerboseThrowOnFailure(Prop.forAll(arbFiltered, FSharpFunc<int, bool>.FromConverter(i => true)));
        }

        [Test]
        public void From()
        {
        }

        [Test]
        public void FromGen()
        {
        }

        [Test]
        public void FromGenShrink()
        {
        }

        [Test]
        public void Generate()
        {
        }

        [Test]
        public void MapFilter()
        {
            var arbNonEmptyString = Arb.Default.NonEmptyString();
            var arbMappedFiltered = Arb.mapFilter(
                FSharpFunc<NonEmptyString, NonEmptyString>.FromConverter(nes => NonEmptyString.NewNonEmptyString(nes.Item.ToUpper())),
                FSharpFunc<NonEmptyString, bool>.FromConverter(nes => nes.Item.Length == 2),
                arbNonEmptyString);
            Check.VerboseThrowOnFailure(Prop.forAll(arbMappedFiltered, FSharpFunc<NonEmptyString, bool>.FromConverter(nes => true)));
        }

        [Test]
        public void Register()
        {
        }

        [Test]
        public void RegisterByType()
        {
        }

        [Test]
        public void Shrink()
        {
        }

        [Test]
        public void ShrinkNumber()
        {
        }
    }
}
