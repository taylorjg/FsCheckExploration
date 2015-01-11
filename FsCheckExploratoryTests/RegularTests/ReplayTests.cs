using System.Collections.Generic;
using FsCheckUtils;
using Microsoft.FSharp.Core;
using NUnit.Framework;
using FsCheck;

namespace FsCheckExploratoryTests.RegularTests
{
    using IntBoolProperty = FSharpFunc<int, bool>;

    [TestFixture]
    internal class ReplayTests
    {
        [Test]
        public void Replay()
        {
            var replay = Random.StdGen.NewStdGen(395461793, 1);
            var config = Config.VerboseThrowOnFailure.WithReplay(replay).WithName("ReplayConfig");
            var property = IntBoolProperty.FromConverter(_ => true);
            var intsFromRun1 = CheckIntBoolProperty(config, property);
            var intsFromRun2 = CheckIntBoolProperty(config, property);
            Assert.That(intsFromRun1, Is.EqualTo(intsFromRun2));
        }

        private static IEnumerable<int> CheckIntBoolProperty(Config config, IntBoolProperty property)
        {
            var ints = new List<int>();

            Check.One(config, IntBoolProperty.FromConverter(i =>
            {
                ints.Add(i);
                return property.Invoke(i);
            }));

            return ints;
        }
    }
}
