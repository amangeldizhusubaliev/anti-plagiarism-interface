using System;
using System.Collections.Generic;
using System.Text;
using anti_plagiarism;

namespace Levenshteinn
{
    class Levenshtein : IPlugin
    {
        public string text1 { get; set; }
        public string text2 { get; set; }
        public object result { get; set; }


        public Levenshtein()
        {
            text1 = "";
            text2 = "";
            result = 0;
        }
        public Levenshtein(string a, string b)
        {
            text1 = a;
            text2 = b;
            result = 0;
        }

        public void Compare()
        {
            int alen = text1.Length;
            int blen = text2.Length;
            int[,] d = new int[2, blen + 1];
            for (int j = 0; j <= blen; j++)
            {
                d[0, j] = j;
            }
            for (int i = 1; i <= alen; i++)
            {
                d[i & 1, 0] = i;
                for (int j = 1; j <= blen; j++)
                {
                    d[i & 1, j] = Math.Min(Math.Min(d[i & 1, j - 1], d[(i & 1) ^ 1, j]) + 1,
                        d[(i & 1) ^ 1, j - 1] + (text1[i - 1] != text2[j - 1] ? 1 : 0));
                }
            }
            result = d[alen & 1, blen];
        }
        public static int Compare(string a, string b)
        {
            int alen = a.Length;
            int blen = b.Length;
            int[,] d = new int[2, blen + 1];
            for (int j = 0; j <= blen; j++)
            {
                d[0, j] = j;
            }
            for (int i = 1; i <= alen; i++)
            {
                d[i & 1, 0] = i;
                for (int j = 1; j <= blen; j++)
                {
                    d[i & 1, j] = Math.Min(Math.Min(d[i & 1, j - 1], d[(i & 1) ^ 1, j]) + 1, 
                        d[(i & 1) ^ 1, j - 1] + (a[i - 1] != b[j - 1] ? 1 : 0));
                }
            }
            return d[alen & 1, blen];
        }
    }
}
