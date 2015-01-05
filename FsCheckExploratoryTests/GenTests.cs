using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests
{
    [TestFixture]
    internal class GenTests
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
            gen.DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenOfArrayOfInt()
        {
            var gen = Arb.from<int[]>().Generator;
            gen.DumpSamples(Formatters.FormatCollection);
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
            Gen.arrayOf(gen).DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenArrayOfLength()
        {
            var gen = Arb.from<int>().Generator;
            Gen.arrayOfLength(5, gen).DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenArray2DOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.array2DOf(gen).DumpSamples(Formatters.Format2DArray);
        }

        [Test]
        public void GenArray2DOfDim()
        {
            var gen = Arb.from<int>().Generator;
            Gen.array2DOfDim(2, 3, gen).DumpSamples(Formatters.Format2DArray);
        }

        [Test]
        public void GenListOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.listOf(gen).DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenNonEmptyListOf()
        {
            var gen = Arb.from<int>().Generator;
            Gen.nonEmptyListOf(gen).DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenListOfLength()
        {
            var gen = Arb.from<int>().Generator;
            Gen.listOfLength(5, gen).DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenSubListOf()
        {
            Gen.subListOf(new[] { "A", "B", "C", "D", "E" }).DumpSamples(Formatters.FormatCollection);
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
            Gen.sequence(gens).DumpSamples(Formatters.FormatCollection);
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

        [Test]
        public void GenMap()
        {
            var intIsPositive = FSharpFunc<int, bool>.FromConverter(i => i >= 0);
            var genPositiveInt = Gen.suchThat(intIsPositive, Arb.Default.Int32().Generator);
            var intToStringOfXs = FSharpFunc<int, string>.FromConverter(i => new string('X', i));
            var genMap = Gen.map(intToStringOfXs, genPositiveInt);
            genMap.DumpSamples();
        }

        [Test]
        public void GenMap2()
        {
            var intIsPositive = FSharpFunc<int, bool>.FromConverter(i => i >= 0);
            var genPositiveInt = Gen.suchThat(intIsPositive, Arb.Default.Int32().Generator);
            var charIsA2Z = FSharpFunc<char, bool>.FromConverter(c => c >= 'A' && c <= 'Z');
            var genCharA2Z = Gen.suchThat(charIsA2Z, Arb.Default.Char().Generator);
            // TODO: we may need some currying helpers...
            var f = FSharpFunc<int, FSharpFunc<char, string>>.FromConverter(i => FSharpFunc<char, string>.FromConverter(c => new string(c, i)));
            var genMap2 = Gen.map2(f, genPositiveInt, genCharA2Z);
            genMap2.DumpSamples();
        }

        [Test]
        public void GenSized()
        {
            var genInt = Arb.from<int>().Generator;
            var genSized = Gen.sized(FSharpFunc<int, Gen<int[]>>.FromConverter(size => Gen.arrayOfLength(size, genInt)));
            genSized.DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenResize()
        {
            var genIntArray = Arb.from<int[]>().Generator;
            var genResize = Gen.resize(20, genIntArray);
            genResize.DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenApply()
        {
            var intToStringOfXs = FSharpFunc<int, string>.FromConverter(i => new string('X', i));
            var genFunc = Gen.constant(intToStringOfXs);
            var intIsPositive = FSharpFunc<int, bool>.FromConverter(i => i >= 0);
            var genPositiveInt = Gen.suchThat(intIsPositive, Arb.Default.Int32().Generator);
            var genApply = Gen.apply(genFunc, genPositiveInt);
            genApply.DumpSamples();
        }

        [Test]
        public void GenApplyTwice()
        {
            var intIsPositive = FSharpFunc<int, bool>.FromConverter(i => i >= 0);
            var genPositiveInt = Gen.suchThat(intIsPositive, Arb.Default.Int32().Generator);
            var charIsA2Z = FSharpFunc<char, bool>.FromConverter(c => c >= 'A' && c <= 'Z');
            var genCharA2Z = Gen.suchThat(charIsA2Z, Arb.Default.Char().Generator);
            // TODO: we may need some currying helpers...
            var func = FSharpFunc<int, FSharpFunc<char, string>>.FromConverter(i => FSharpFunc<char, string>.FromConverter(c => new string(c, i)));
            var genFunc1 = Gen.constant(func);
            var genFunc2 = Gen.apply(genFunc1, genPositiveInt);
            var genApply = Gen.apply(genFunc2, genCharA2Z);
            genApply.DumpSamples();
        }

        private static readonly string[] BookTitles = new[]
            {
                "Harry Potter and the Philosopher's Stone",
                "Harry Potter and the Chamber of Secrets",
                "Harry Potter and the Prisoner of Azkaban",
                "Harry Potter and the Goblet of Fire",
                "Harry Potter and the Order of the Phoenix"
            };

        [Test]
        public void GenMultipleBookTitlesUsingQuerySyntax()
        {
            var genBookTitle = Gen.elements(BookTitles);
            var genNumBooks = Gen.choose(0, 5);
            var genMultipleBookTitles =
                from title in genBookTitle
                from n in genNumBooks
                select Enumerable.Repeat(title, n);
            genMultipleBookTitles.DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenMultipleBookTitlesUsingMethodSyntax()
        {
            var genBookTitle = Gen.elements(BookTitles);
            var genNumBooks = Gen.choose(0, 5);
            var genMultipleBookTitles = genBookTitle.SelectMany(title => genNumBooks, Enumerable.Repeat);
            genMultipleBookTitles.DumpSamples(Formatters.FormatCollection);
        }

        [Test]
        public void GenMultipleBookTitlesUsingDirectGenBuilderCalls()
        {
            var genBookTitle = Gen.elements(BookTitles);
            var genNumBooks = Gen.choose(0, 5);
            var gb = GenBuilder.gen;
            var genMultipleBookTitles = gb.Bind(genBookTitle, FSharpFunc<string, Gen<IEnumerable<string>>>.FromConverter(
                title => gb.Bind(genNumBooks, FSharpFunc<int, Gen<IEnumerable<string>>>.FromConverter(
                    n => gb.Return(Enumerable.Repeat(title, n))))));
            genMultipleBookTitles.DumpSamples(Formatters.FormatCollection);
        }

        // TODO: Custom generator for a custom type e.g. Employee
    }
}
