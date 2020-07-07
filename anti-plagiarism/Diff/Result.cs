using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Diff
{
    public class Result
    {
        public static void CreateResult(string a, string b)
        {
            string[] blocks = LCSComparing.Compare(a, b);

            string html = "<h4><strong>File:" + Path.GetFileName(b) + " VS File: " + Path.GetFileName(b) + "</strong></h4>";
            html += "<h4> Created: " + DateTime.Now.ToString("G") + "</h4>";
            html += "<h3> Plagiarism Probability: " + blocks[2] + " %  </h3>";
            html += "<h5> Suspected File:</h5>";
            html += blocks[0];
            html += "<h5> File: " + Path.GetFileName(b) + "</h5>";
            html += blocks[1];
            File.WriteAllText("result.htm", html);
        }
    }
}
