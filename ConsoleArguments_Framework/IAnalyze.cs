using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArguments {

    internal interface IAnalyze {

        object obj { get; set; }
        void Mapping();
        bool SearchFlag(char flag, IEnumerator<string> enumerator);
        bool SearchFillName(string fullName, IEnumerator<string> enumerator);

    }

}
