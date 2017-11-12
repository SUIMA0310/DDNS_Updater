using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArguments.Attributes {

    public class FlugAttribute : ArgumentAttribute{

        public FlugAttribute(char flag) : base( flag ) { }
        public FlugAttribute(string fullName) : base( fullName ) { }
        public FlugAttribute(char flag, string fullName) : base( flag, fullName ) { }

    }

}
