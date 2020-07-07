using System;
using Diff;
using Levenshteinn;

namespace anti_plagiarism
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lev = new Levenshtein("abc", "bca");
            string a = "5a1b3cde";
            string b = "6a2b3cee";

            var ans = Levenshtein.Compare(a, b);
            var res = LCSComparing.Compare(a, b);

            Console.WriteLine(ans);
            Console.WriteLine(res[2]);
        }
    }
}
