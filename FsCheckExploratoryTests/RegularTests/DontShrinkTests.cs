using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace FsCheckExploratoryTests.RegularTests
{
    using Property = Gen<Rose<Result>>;

    [TestFixture]
    public class DontShrinkTests
    {
        private static readonly Config Config = Config.VerboseThrowOnFailure;

        [Test]
        public void Test()
        {
            var arb = Arb.Default.DontShrink<IList<int>>();
            var body = FSharpFunc<DontShrink<IList<int>>, bool>.FromConverter(dsxs =>
            {
                var xs = dsxs.Item;
                return xs.Reverse().Reverse().SequenceEqual(xs);
            });
            Check.One(Config, Prop.forAll(arb, body));
        }

        [FsCheck.NUnit.Property(Verbose = true)]
        public Property Property(DontShrink<IList<int>> dsxsParam)
        {
            var generator = Any.Value(dsxsParam).Select(dsxs => dsxs.Item);
            Func<IList<int>, bool> assertion = xs =>
                xs.Reverse().Reverse().SequenceEqual(xs);
            return Spec.For(generator, assertion).Build();
        }
    }
}
