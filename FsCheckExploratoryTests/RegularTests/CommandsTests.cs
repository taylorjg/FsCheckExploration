using System;
using FsCheck;
using FsCheck.Fluent;
using NUnit.Framework;

namespace FsCheckExploratoryTests.RegularTests
{
    internal class Counter
    {
        private int _n;
        public void Inc() { _n++; }
        // To force failures, use the version of Dec() on the next line.
        // public void Dec() { _n = _n - ((_n > 2) ? 2 : 1); }
        public void Dec() { _n--; }
        public int Get() { return _n; }
        public void Reset() { _n = 0; }
        public override string ToString() { return Convert.ToString(_n); }
    }

    internal class CounterSpec : Commands.ISpecification<Counter, int>
    {
        private class Inc : Commands.ICommand<Counter, int>
        {
            public override Counter RunActual(Counter c) { c.Inc(); return c; }
            public override int RunModel(int m) { return m + 1; }
            public override Gen<Rose<Result>> Post(Counter c, int m) { return Prop.ofTestable(m == c.Get()); }
            public override string ToString() { return "Inc"; }
        }

        private class Dec : Commands.ICommand<Counter, int>
        {
            public override Counter RunActual(Counter c) { c.Dec(); return c; }
            public override int RunModel(int m) { return m - 1; }
            public override Gen<Rose<Result>> Post(Counter c, int m) { return Prop.ofTestable(m == c.Get()); }
            public override string ToString() { return "Dec"; }
        }

        public Tuple<Counter, int> Initial() { return Tuple.Create(new Counter(), 0); }

        public Gen<Commands.ICommand<Counter, int>> GenCommand(int _)
        {
            var g1 = new Inc() as Commands.ICommand<Counter, int>;
            var g2 = new Dec() as Commands.ICommand<Counter, int>;
            return Any.ValueIn(g1, g2);
        }
    }

    [TestFixture]
    internal class CommandsTests
    {
        [Test]
        public void AsProperty()
        {
            var spec = new CounterSpec();
            var property = Commands.asProperty(spec);
            Check.QuickThrowOnFailure(property);
        }
    }
}
