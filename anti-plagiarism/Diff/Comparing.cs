using anti_plagiarism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Diff
{
    public  class LCSComparing : IPlugin
    {
        public string text1 { get; set; }
        public string text2 { get; set; }
        public object result { get; set ;}

        static string[] block = new string[3];

        public LCSComparing()
        {
            text1 = "";
            text2 = "";
            result = 0.0;
        }
        public LCSComparing(string a, string b)
        {
            text1 = a;
            text2 = b;
            result = 0.0;
            if(System.IO.File.Exists(a) && System.IO.File.Exists(b))
            {
                text1 = System.IO.File.ReadAllText(a);
                text2 = System.IO.File.ReadAllText(b);
                Compare();
                result = block[2];
            }
            else
            {
                text1 = a;
                text2 = b;
                Compare();
                result = block[2];
            }
        }
        
        public static int[] DiffCharCodes(string aText, bool ignoreCase)
        {
            int[] Codes;

            if (ignoreCase)
                aText = aText.ToUpperInvariant();

            Codes = new int[aText.Length];

            for (int n = 0; n < aText.Length; n++)
                Codes[n] = (int)aText[n];

            return (Codes);
        } // DiffCharCodes
        public static string[] Compare(string text, string test)
        {
            Diff.Item[] diffs = Diff.DiffInt(DiffCharCodes(text, false), DiffCharCodes(test, false));
            //
            string[] answers = new string[3];
            answers[0] = "<pre>";
            answers[1] = "<pre>";
            string bgGreen = "<span style = \"background: #64FF00\">";
            string bgRed = "<span style = \"background: #FF3D3D\">";
            string bgYellow = "<span style = \"background: #FFFF00\">";
            string endOfSpan = "</span>";
            string endOfLine = "<br/>";
            double confidence = 0;
            int pos;
            int pos1 = 0;
            int pos2 = 0;
            for (int n = 0; n < diffs.Length; n++)
            {
                Diff.Item it = diffs[n];
                answers[0] += bgGreen;
                answers[1] += bgGreen;
                for (; (pos1 < it.StartA) && (pos1 < text.Length); pos1++)
                {
                    if (text[pos1] == '\n')
                    {
                        answers[0] += endOfLine;
                    }
                    else
                    {
                        answers[0] += text[pos1];
                    }
                }
                for (; (pos2 < it.StartB) && (pos2 < test.Length); pos2++)
                {
                    if (test[pos2] == '\n')
                    {
                        answers[1] += endOfLine;
                    }
                    else
                    {
                        answers[1] += test[pos2];
                    }
                }
                answers[0] += endOfSpan;
                answers[1] += endOfSpan;

                if (it.deletedA > 0)
                {
                    confidence += it.deletedA;
                    pos = pos1 + it.deletedA;
                    answers[0] += bgRed;

                    for (; pos1 < pos; pos1++)
                    {
                        if (text[pos1] == '\n')
                            answers[0] += endOfLine;
                        else
                            answers[0] += text[pos1];
                    }
                    answers[0] += endOfSpan;
                }

                if (pos2 < it.StartB + it.insertedB)
                {
                    confidence += it.insertedB;
                    answers[1] += bgYellow;
                    for (; pos2 < it.StartB + it.insertedB; pos2++)
                    {
                        if (test[pos2] == '\n')
                            answers[1] += endOfLine;
                        else
                            answers[1] += test[pos2];
                    }
                    answers[1] += endOfSpan;
                }
            }
            answers[0] += bgGreen;
            answers[1] += bgGreen;
            for (; pos1 < text.Length; pos1++)
            {
                if (text[pos1] == '\n')
                    answers[0] += endOfLine;
                else
                    answers[0] += text[pos1];
            }
            for (; pos2 < test.Length; pos2++)
            {
                if (test[pos2] == '\n')
                    answers[1] += endOfLine;
                else
                    answers[1] += test[pos2];
            }
            answers[0] += endOfSpan;
            answers[1] += endOfSpan;
            answers[0] += "</pre>";
            answers[1] += "</pre>";
            answers[2] = ((1.0 - Math.Min(1.0, (confidence / test.Length))) * 100.0).ToString();
            return answers;
        }

        public void Compare()
        {
            Diff.Item[] diffs = Diff.DiffInt(DiffCharCodes(text1, false), DiffCharCodes(text2, false));
            //
            //string[] block = new string[3];
            block[0] = "<pre>";
            block[1] = "<pre>";
            string bgGreen = "<span style = \"background: #64FF00\">";
            string bgRed = "<span style = \"background: #FF3D3D\">";
            string bgYellow = "<span style = \"background: #FFFF00\">";
            string endOfSpan = "</span>";
            string endOfLine = "<br/>";
            double confidence = 0;
            int pos;
            int pos1 = 0;
            int pos2 = 0;
            for (int n = 0; n < diffs.Length; n++)
            {
                Diff.Item it = diffs[n];
                block[0] += bgGreen;
                block[1] += bgGreen;
                for (; (pos1 < it.StartA) && (pos1 < text1.Length); pos1++)
                {
                    if (text1[pos1] == '\n')
                    {
                        block[0] += endOfLine;
                    }
                    else
                    {
                        block[0] += text1[pos1];
                    }
                }
                for (; (pos2 < it.StartB) && (pos2 < text2.Length); pos2++)
                {
                    if (text2[pos2] == '\n')
                    {
                        block[1] += endOfLine;
                    }
                    else
                    {
                        block[1] += text2[pos2];
                    }
                }
                block[0] += endOfSpan;
                block[1] += endOfSpan;

                if (it.deletedA > 0)
                {
                    confidence += it.deletedA;
                    pos = pos1 + it.deletedA;
                    block[0] += bgRed;

                    for (; pos1 < pos; pos1++)
                    {
                        if (text1[pos1] == '\n')
                            block[0] += endOfLine;
                        else
                            block[0] += text1[pos1];
                    }
                    block[0] += endOfSpan;
                }

                if (pos2 < it.StartB + it.insertedB)
                {
                    confidence += it.insertedB;
                    block[1] += bgYellow;
                    for (; pos2 < it.StartB + it.insertedB; pos2++)
                    {
                        if (text2[pos2] == '\n')
                            block[1] += endOfLine;
                        else
                            block[1] += text2[pos2];
                    }
                    block[1] += endOfSpan;
                }
            }
            block[0] += bgGreen;
            block[1] += bgGreen;
            for (; pos1 < text1.Length; pos1++)
            {
                if (text1[pos1] == '\n')
                    block[0] += endOfLine;
                else
                    block[0] += text1[pos1];
            }
            for (; pos2 < text2.Length; pos2++)
            {
                if (text2[pos2] == '\n')
                    block[1] += endOfLine;
                else
                    block[1] += text2[pos2];
            }
            block[0] += endOfSpan;
            block[1] += endOfSpan;
            block[0] += "</pre>";
            block[1] += "</pre>";
            block[2] = ((1.0 - Math.Min(1.0, (confidence / text2.Length))) * 100.0).ToString();
            //result = ((1.0 - Math.Min(1.0, (confidence / text2.Length))) * 100.0).ToString();
        }
    }
}