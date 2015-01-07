using System;
using FsCheck;
using FsCheck.Fluent;
using Microsoft.FSharp.Collections;

namespace FsCheckExploratoryTests.Utils
{
    internal static class ConfigurationExtensions
    {
        public static Configuration ToConfiguration(this Config config)
        {
            var configuration = new Configuration
                {
                    MaxNbOfTest = config.MaxTest,
                    MaxNbOfFailedTests = config.MaxFail,
                    Name = config.Name,
                    StartSize = config.StartSize,
                    EndSize = config.EndSize,
                    Runner = config.Runner
                };

            var everyFSharpFunc = config.Every;
            Func<int, object[], string> everyFunc = (n, args) => everyFSharpFunc.Invoke(n).Invoke(ListModule.OfSeq(args));
            configuration.Every = everyFunc;

            var everyShrinkFSharpFunc = config.EveryShrink;
            Func<object[], string> everyShrinkFunc = args => everyShrinkFSharpFunc.Invoke(ListModule.OfSeq(args));
            configuration.EveryShrink = everyShrinkFunc;

            return configuration;
        }
    }
}
