using System.Collections.Generic;
using FsCheck;
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
        }

        [Test]
        public void Given()
        {
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
        }
    }
}
