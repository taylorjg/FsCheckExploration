using System;
using System.Collections.Generic;
using FsCheck;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests
{
    [TestFixture]
    internal class GenSamples
    {
        [Test]
        public void GenOfString()
        {
            var gen = Arb.from<string>().Generator;
            gen.DumpSamples();
        }

        [Test]
        public void GenOfDateTime()
        {
            var gen = Arb.from<DateTime>().Generator;
            gen.DumpSamples();
        }

        [Test]
        public void GenOfInt()
        {
            var gen = Arb.from<int>().Generator;
            gen.DumpSamples();
        }

        [Test]
        public void GenOfListOfInt()
        {
            var gen = Arb.from<IList<int>>().Generator;
            gen.DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenOfArrayOfInt()
        {
            var gen = Arb.from<int[]>().Generator;
            gen.DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenConstant()
        {
            var gen = Gen.constant("Hobson's Choice");
            gen.DumpSamples();
        }

        [Test]
        public void GenElements()
        {
            var gen = Gen.elements(new[] { "A", "B", "C", "D", "E" });
            gen.DumpSamples();
        }

        [Test]
        public void GenOneOf()
        {
            var gen = Gen.oneof(new[]
                {
                    Gen.constant("A"),
                    Gen.constant("B"),
                    Gen.constant("C"),
                    Gen.constant("D"),
                    Gen.constant("E")
                });
            gen.DumpSamples();
        }

        [Test]
        public void GenFrequency()
        {
            var gen = Gen.frequency(new[]
                {
                    Tuple.Create(80, Gen.constant("A")),
                    Tuple.Create(5, Gen.constant("B")),
                    Tuple.Create(5, Gen.constant("C")),
                    Tuple.Create(5, Gen.constant("D")),
                    Tuple.Create(5, Gen.constant("E"))
                });
            gen.DumpSamples();
        }

        [Test]
        public void GenChoose()
        {
            var gen = Gen.choose(10, 15);
            gen.DumpSamples();
        }

        [Test]
        public void GenArrayOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.arrayOf(gen).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenArrayOfLength()
        {
            var gen = Arb.from<int>().Generator;
            Gen.arrayOfLength(5, gen).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenArray2DOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.array2DOf(gen).DumpSamples(arr => Formatters.Format2DArray(arr));
        }

        [Test]
        public void GenArray2DOfDim()
        {
            var gen = Arb.from<int>().Generator;
            Gen.array2DOfDim(2, 3, gen).DumpSamples(arr => Formatters.Format2DArray(arr));
        }

        [Test]
        public void GenListOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.listOf(gen).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenNonEmptyListOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.nonEmptyListOf(gen).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenListOfLength()
        {
            var gen = Arb.from<int>().Generator;
            Gen.listOfLength(5, gen).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenSubListOf()
        {
            Gen.subListOf(new[] { "A", "B", "C", "D", "E" }).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenTwo()
        {
            var gen = Arb.from<int>().Generator;
            Gen.two(gen).DumpSamples();
        }

        [Test]
        public void GenThree()
        {
            var gen = Arb.from<int>().Generator;
            Gen.three(gen).DumpSamples();
        }

        [Test]
        public void GenFour()
        {
            var gen = Arb.from<int>().Generator;
            Gen.four(gen).DumpSamples();
        }

        [Test]
        public void GenSequence()
        {
            var gen1 = Gen.choose(10, 19);
            var gen2 = Gen.choose(20, 29);
            var gen3 = Gen.choose(30, 39);
            var gens = ListModule.OfSeq(new[] { gen1, gen2, gen3 });
            Gen.sequence(gens).DumpSamples(xs => Formatters.FormatCollection(xs));
        }

        [Test]
        public void GenSuchThat()
        {
            var genInt = Arb.from<int>().Generator;
            var genSuchThat = Gen.suchThat(FSharpFunc<int, bool>.FromConverter(i => i > 10 && i < 20), genInt);
            genSuchThat.DumpSamples();
        }

        [Test]
        public void GenSuchThatOption()
        {
            var genInt = Arb.from<int>().Generator;
            var genSuchThatOption = Gen.suchThatOption(FSharpFunc<int, bool>.FromConverter(i => i > 10 && i < 20), genInt);
            genSuchThatOption.DumpSamples();
        }

        // Gen.map
        // Gen.map2
        // Gen.map3
        // Gen.map4
        // Gen.map5
        // Gen.map6

        // Gen.resize

        // Gen.sized

        // Gen.apply

        // - custom generators
        //  - e.g. list of valid rolls for bowling
        //  - for custom type e.g. Employee
    }
}
