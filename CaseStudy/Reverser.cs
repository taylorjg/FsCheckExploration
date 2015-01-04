using System.Linq;

namespace CaseStudy
{
    public static class Reverser
    {
        public static string Reverse(this string s)
        {
            return s == null ? null : new string(s.ToCharArray().Reverse().ToArray());
        }
    }
}
