using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace anti_plagiarism
{
    interface IPlugin
    {
        string text1 { get; set; }
        string text2 { get; set; }
        object result { get; set; }

        void Compare();
    }
}
