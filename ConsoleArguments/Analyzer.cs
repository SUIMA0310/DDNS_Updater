using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace ConsoleArguments {
    public class Analyzer {

        public static void Analyze(object obj, string[] args) {

            var ins = new Analyzer { Obj = obj, Args = args };
            ins.Analyzing();

        }

        public object Obj { get; set; }
        public string[] Args { get; set; }

        

        public void Analyzing() {

            #region Error Check

            if ( Obj == null ) {

                throw new ArgumentNullException( $"{nameof(Obj)}がnullです" );

            }

            if ( Args == null ) {

                throw new ArgumentNullException( $"{nameof( Args )}がnullです" );

            }

            #endregion

            var analyzes = GetAnalyzes();
            foreach ( var analyze in analyzes ) {
                analyze.obj = Obj;
                analyze.Mapping();
            }

            using ( var enumerator = ((IEnumerable<string>)Args).GetEnumerator() ) {

                while ( enumerator.MoveNext() ) {

                    var str = enumerator.Current;
                    if ( !IsSwitch( str ) ) { continue; }

                    if ( IsFlag( str ) ) {

                        foreach ( var analyze in analyzes ) {

                            if( analyze.SearchFlag( GetSwitch(str)[0], enumerator ) ) {
                                break;
                            }

                        }

                    } else {

                        foreach ( var analyze in analyzes ) {

                            if ( analyze.SearchFillName( GetSwitch( str ), enumerator )) {
                                break;
                            }

                        }

                    }

                }

            }


        }

        private IAnalyze[] GetAnalyzes() {

            return new IAnalyze[] {
                new PropertiesAnalyze(),
                new MethodAnalyze()
            };

        }

        private bool IsSwitch( string arg ) {

            return arg.StartsWith("/") && arg.Length > 1;

        }

        private bool IsFlag( string arg ) {

            return arg.Length == 2;

        }

        private string GetSwitch( string arg ) {

            return arg.Substring( 1 );

        }

    }
}
