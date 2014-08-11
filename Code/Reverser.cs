using System.Linq;

namespace Code
{
    public static class Reverser
    {
        public static string Reverse(this string s)
        {
            if (s == null) return null;

            // Introduce a deliberate error!
            var s2 = (s.Length > 53) ? s.ToUpper() : s;

            return new string(s2.ToCharArray().Reverse().ToArray());
        }
    }
}
