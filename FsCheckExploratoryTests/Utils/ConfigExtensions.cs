using FsCheck;
using Microsoft.FSharp.Core;

namespace FsCheckExploratoryTests.Utils
{
    internal static class ConfigExtensions
    {
        public static Config WithReplay(this Config config, Random.StdGen replay)
        {
            return new Config(
                config.MaxTest,
                config.MaxFail,
                new FSharpOption<Random.StdGen>(replay), 
                config.Name,
                config.StartSize,
                config.EndSize,
                config.Every,
                config.EveryShrink,
                config.Arbitrary,
                config.Runner);
        }

        public static Config WithName(this Config config, string name)
        {
            return new Config(
                config.MaxTest,
                config.MaxFail,
                config.Replay,
                name,
                config.StartSize,
                config.EndSize,
                config.Every,
                config.EveryShrink,
                config.Arbitrary,
                config.Runner);
        }
    }
}
