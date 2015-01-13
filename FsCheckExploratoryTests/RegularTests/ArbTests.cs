using FsCheckExploratoryTests.Utils;
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

        private class MyIntArbitrary : Arbitrary<int>
        {
            public override Gen<int> Generator
            {
                get { return Gen.choose(10, 15); }
            }
        }

        // ReSharper disable ClassNeverInstantiated.Local
        private class MyArbitraries
        {
            // ReSharper disable UnusedMember.Local
            // ReSharper disable MemberHidesStaticFromOuterClass
            public static Arbitrary<int> MyIntArbitrary()
            {
                return new MyIntArbitrary();
            }
            // ReSharper restore MemberHidesStaticFromOuterClass
            // ReSharper restore UnusedMember.Local
        }
        // ReSharper restore ClassNeverInstantiated.Local

        [Test]
        public void Generate()
        {
            // Registers an Arbitrary<int> that produces values between 10-15 inclusive.
            Arb.register<MyArbitraries>();

            // This retrieves the special Arbitrary<int> that we just registered.
            var gen1 = Arb.generate<int>();
            gen1.DumpSamples();

            // This retrieves the default Arbitrary<int>.
            var gen2 = Arb.Default.Int32().Generator;
            gen2.DumpSamples();
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
