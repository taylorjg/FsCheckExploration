using System;
using FsCheck;
using Microsoft.FSharp.Collections;
using NUnit.Framework;
using FsCheck.Fluent;

namespace FsCheckExploratoryTests.FluentTests
{
    [TestFixture]
    internal class FluentConfigurationTests
    {
        [Test]
        public void MaxNbOfTest()
        {
            // TODO: could do with a helper to convert an FSharpFunc to a System.Func (especially for a multi-parameter function)
            var everyVerboseFSharpFunc = Config.Verbose.Every;
            Func<int, object[], string> everyVerboseFunc = (n, args) => everyVerboseFSharpFunc.Invoke(n).Invoke(ListModule.OfSeq(args));

            Spec
                .ForAny((int i) => true)
                // TODO: could add a helper to construct a Configuration from a Config
                .Check(new Configuration {Name = "Demo", MaxNbOfTest = 50, Every = everyVerboseFunc});
        }
    }
}
