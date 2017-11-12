using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ConsoleArguments {

    internal class MethodAnalyze : IAnalyze {

        private Dictionary<char, MethodInfo> flagDic = new Dictionary<char, MethodInfo>();
        private Dictionary<string, MethodInfo> fullNameDic = new Dictionary<string, MethodInfo>();

        public object obj { get; set; }

        public void Mapping() {

            var type = obj.GetType();
            var methods = type.GetMethods();

            foreach ( var method in methods ) {

                var attribule = method.GetCustomAttribute<Attributes.ArgumentAttribute>();
                if ( attribule == null ) { continue; }

                if ( attribule.Flag != null ) {

                    flagDic.Add( (char)attribule.Flag, method );

                }
                if ( attribule.FullName != null ) {

                    fullNameDic.Add( attribule.FullName, method );

                }

            }

        }

        public bool SearchFillName(string fullName, IEnumerator<string> enumerator) {
            if ( !fullNameDic.ContainsKey( fullName ) ) { return false; }
            InvokeMethod( fullNameDic[fullName] );
            return true;
        }

        public bool SearchFlag(char flag, IEnumerator<string> enumerator) {
            if ( !flagDic.ContainsKey( flag ) ) { return false; }
            InvokeMethod( flagDic[flag] );
            return true;
        }

        private void InvokeMethod(MethodInfo method) {

            method.Invoke( obj, new object[] { } );

        }
    }

}
