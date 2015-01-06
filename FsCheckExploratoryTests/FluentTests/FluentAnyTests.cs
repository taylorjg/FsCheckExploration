using System;
using System.Linq;
using NUnit.Framework;
using FsCheck;
using FsCheck.Fluent;
using FsCheckExploratoryTests.Utils;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentAnyTests
    {
        [Test, Description("Same as Gen.oneof")]
        public void GeneratorInEnumerable()
        {
            var generators = new[]
                {
                    Gen.constant("A"),
                    Gen.constant("B"),
                    Gen.constant("C"),
                    Gen.constant("D"),
                    Gen.constant("E")
                }.AsEnumerable();
            var gen = Any.GeneratorIn(generators);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.oneof")]
        public void GeneratorInParamsArray()
        {
            var gen = Any.GeneratorIn(
                Gen.constant("A"),
                Gen.constant("B"),
                Gen.constant("C"),
                Gen.constant("D"),
                Gen.constant("E"));
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.choose")]
        public void IntBetween()
        {
            var gen = Any.IntBetween(10, 15);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.sized")]
        public void OfSize()
        {
            var genInt = Any.OfType<int>();
            var gen = Any.OfSize(size => Gen.arrayOfLength(size, genInt));
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Arb.from<>().Generator")]
        public void OfType()
        {
            var gen = Any.OfType<Tuple<char, double, bool, DateTime>>();
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.sequence")]
        public void SequenceOfEnumerable()
        {
            var gen1 = Gen.choose(10, 19);
            var gen2 = Gen.choose(20, 29);
            var gen3 = Gen.choose(30, 39);
            var generators = new[] {gen1, gen2, gen3}.AsEnumerable();
            var gen = Any.SequenceOf(generators);
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Gen.sequence")]
        public void SequenceOfParamsArray()
        {
            var gen1 = Gen.choose(10, 19);
            var gen2 = Gen.choose(20, 29);
            var gen3 = Gen.choose(30, 39);
            var gen = Any.SequenceOf(gen1, gen2, gen3);
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test, Description("Same as Gen.constant")]
        public void Value()
        {
            var gen = Any.Value('X');
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.elements")]
        public void ValueInEnumerable()
        {
            var values = new[] {1, 2, 3, 4, 5}.AsEnumerable();
            var gen = Any.ValueIn(values);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.elements")]
        public void ValueInParamsArray()
        {
            var gen = Any.ValueIn(1, 2, 3, 4, 5);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.frequency over some generators")]
        public void WeighedGeneratorInEnumerable()
        {
            var weighedValues = new[]
                {
                    new WeightAndValue<Gen<int>>(80, Gen.choose(0, 9)),
                    new WeightAndValue<Gen<int>>(5, Gen.choose(10, 19)),
                    new WeightAndValue<Gen<int>>(5, Gen.choose(20, 29)),
                    new WeightAndValue<Gen<int>>(5, Gen.choose(30, 39)),
                    new WeightAndValue<Gen<int>>(5, Gen.choose(40, 49))
                }.AsEnumerable();
            var gen = Any.WeighedGeneratorIn(weighedValues);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.frequency over some generators")]
        public void WeighedGeneratorInParamsArray()
        {
            var gen = Any.WeighedGeneratorIn(
                new WeightAndValue<Gen<int>>(80, Gen.choose(0, 9)),
                new WeightAndValue<Gen<int>>(5, Gen.choose(10, 19)),
                new WeightAndValue<Gen<int>>(5, Gen.choose(20, 29)),
                new WeightAndValue<Gen<int>>(5, Gen.choose(30, 39)),
                new WeightAndValue<Gen<int>>(5, Gen.choose(40, 49)));
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.frequency over some constant values")]
        public void WeighedValueInEnumerable()
        {
            var weighedValues = new[]
                {
                    new WeightAndValue<string>(80, "A"),
                    new WeightAndValue<string>(5, "B"),
                    new WeightAndValue<string>(5, "C"),
                    new WeightAndValue<string>(5, "D"),
                    new WeightAndValue<string>(5, "E")
                }.AsEnumerable();
            var gen = Any.WeighedValueIn(weighedValues);
            gen.DumpSamples();
        }

        [Test, Description("Same as Gen.frequency over some constant values")]
        public void WeighedValueInParamsArray()
        {
            var gen = Any.WeighedValueIn(
                new WeightAndValue<string>(80, "A"),
                new WeightAndValue<string>(5, "B"),
                new WeightAndValue<string>(5, "C"),
                new WeightAndValue<string>(5, "D"),
                new WeightAndValue<string>(5, "E"));
            gen.DumpSamples();
        }
    }
}
