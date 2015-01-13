using FsCheck.Xunit;
using Xunit;

namespace xUnitTest
{
    public class FirstTest
    {
        [Property(Verbose = true, MaxTest = 10)]
        public void PassingTest(int x)
        {
            Assert.Equal(x * 2, x + x);
        }

        [Property(Verbose = true, MaxTest = 10)]
        public void FailingTest(int x)
        {
            Assert.Equal(x * 2, x + x + 1);
        }
    }
}
