using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheckExploratoryTests.Utils;
using FsCheckUtils;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests.RegularTests
{
    using Property = Gen<Rose<Result>>;
    
    [TestFixture]
    internal class PropTests
    {
        [Test]
        public void Classify()
        {
            var arb = Arb.from<List<int>>();
            var body = FSharpFunc<List<int>, Property>.FromConverter(
                xs =>
                    Prop.classify<Property>(xs.Count <= 1, "xs.Count <= 1").Invoke(
                        Prop.classify<Property>(xs.Count >= 2 && xs.Count <= 5, "xs.Count >= 2 && xs.Count <= 5").Invoke(
                            Prop.classify<Property>(xs.Count > 5, "xs.Count > 5").Invoke(Prop.ofTestable(true)))));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Classify2()
        {
            var arb = Arb.from<List<int>>();
            var body = FSharpFunc<List<int>, Property>.FromConverter(xs =>
            {
                var p0 = Prop.ofTestable(true);
                var p1 = Prop.classify<Property>(xs.Count <= 1, "xs.Count <= 1");
                var p2 = Prop.classify<Property>(xs.Count >= 2 && xs.Count <= 5, "xs.Count >= 2 && xs.Count <= 5");
                var p3 = Prop.classify<Property>(xs.Count > 5, "xs.Count > 5");
                return p3.Invoke(p2.Invoke(p1.Invoke(p0)));
            });
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Classify3()
        {
            var arb = Arb.from<List<int>>();
            var body = FSharpFunc<List<int>, Property>.FromConverter(xs =>
            {
                var p1 = Prop.classify<Property>(xs.Count <= 1, "xs.Count <= 1");
                var p2 = Prop.classify<Property>(xs.Count >= 2 && xs.Count <= 5, "xs.Count >= 2 && xs.Count <= 5");
                var p3 = Prop.classify<Property>(xs.Count > 5, "xs.Count > 5");
                return ChainProperties(true, p1, p2, p3);
            });
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        // TODO: Move this into FsCheckUtils ?
        private static Property ChainProperties(bool b, params FSharpFunc<Property, Property>[] ps)
        {
            return ChainProperties(Prop.ofTestable(b), ps);
        }

        // TODO: Move this into FsCheckUtils ?
        private static Property ChainProperties(Property p0, params FSharpFunc<Property, Property>[] ps)
        {
            return ps.Aggregate(p0, (acc, p) => p.Invoke(acc));
        }

        [Test]
        public void Collect()
        {
            var arb = Arb.from<List<int>>();
            var body = FSharpFunc<List<int>, Property>.FromConverter(
                xs => Prop.collect<string, bool>(string.Format("xs.Count = {0}", xs.Count)).Invoke(true));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void ForAll()
        {
            var arb = Arb.from<int>();
            var body = FSharpFunc<int, bool>.FromConverter(
                n => Math.Abs(n) >= 0);
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Given()
        {
            var arb = Arb.from<int>();
            var body = FSharpFunc<int, Property>.FromConverter(
                n => Prop.given(n >= 0, n + n >= 0, n + n < 0));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Label()
        {
            var arb = Arb.fromGen(Gen.choose(1, 10));
            var body = FSharpFunc<int, Property>.FromConverter(
                n => Prop.label<bool>("n*n <= 100").Invoke(n*n <= 100));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void OfTestable()
        {
            var arb = Arb.from<int>();
            var body = FSharpFunc<int, Property>.FromConverter(
                n => Prop.ofTestable(true));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Throws()
        {
            var arb = Arb.fromGen(Gen.constant(0));
                var body = FSharpFunc<int, Property>.FromConverter(
                n => Prop.throws<DivideByZeroException, bool>(new Lazy<bool>(() => 10/n > 0)));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Trivial()
        {
            var arb = Arb.from<List<int>>();
            var body = FSharpFunc<List<int>, Property>.FromConverter(
                xs => Prop.trivial<bool>(xs.Count <= 1).Invoke(true));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        [Test]
        public void Within()
        {
            var arb = Arb.from<int>();
            var body = FSharpFunc<int, Property>.FromConverter(
                n => Prop.within(50, new Lazy<bool>(() => Math.Abs(n) >= 0)));
            var property = Prop.forAll(arb, body);
            Check.One(Config.QuickThrowOnFailure, property);
        }

        private static List<int> Insert(int x, List<int> xs)
        {
            // TODO: actually insert x into xs
            return xs;
        }

        private static readonly FSharpFunc<int, FSharpFunc<List<int>, Property>> InsertCombined =
            FSharpFunc<int, FSharpFunc<List<int>, Property>>.FromConverter(x =>
                FSharpFunc<List<int>, Property>.FromConverter(xs =>
                {
                    var p0 = PropExtensions.Implies(xs.IsOrdered(), Insert(x, xs).IsOrdered());
                    var p1 = Prop.classify<Property>(new[] {x}.Concat(xs).IsOrdered(), "at-head");
                    var p2 = Prop.classify<Property>(xs.Concat(new[] {x}).IsOrdered(), "at-tail");
                    var p3 = Prop.collect<int, Property>(xs.Count);
                    return ChainProperties(p0, p1, p2, p3);
                }));

        [Test]
        public void InsertCombinedTest()
        {
            Check.One(Config.VerboseThrowOnFailure.WithEndSize(10), InsertCombined);
        }
    }
}
