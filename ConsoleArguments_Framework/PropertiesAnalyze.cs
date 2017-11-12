using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ConsoleArguments {

    internal class PropertiesAnalyze : IAnalyze {

        private Dictionary<char, PropertyInfo>   flagDic     = new Dictionary<char, PropertyInfo>();
        private Dictionary<string, PropertyInfo> fullNameDic = new Dictionary<string, PropertyInfo>();

        public object obj { get; set; }

        public void Mapping() {
            var type = obj.GetType();
            var props = type.GetProperties();

            foreach ( var property in props ) {

                var attribule = property.GetCustomAttribute<Attributes.ArgumentAttribute>();
                if ( attribule == null ) { continue; }

                if ( attribule.Flag != null ) {

                    flagDic.Add( (char)attribule.Flag, property );

                }
                if ( attribule.FullName != null ) {

                    fullNameDic.Add( attribule.FullName, property );

                }

            }
        }

        public bool SearchFillName(string fullName, IEnumerator<string> enumerator) {

            if( !fullNameDic.ContainsKey(fullName) ) { return false; }
            SetValue( fullNameDic[fullName], enumerator );
            return true;

        }

        public bool SearchFlag(char flag, IEnumerator<string> enumerator) {

            if ( !flagDic.ContainsKey( flag ) ) { return false; }
            SetValue( flagDic[flag], enumerator );
            return true;

        }

        private void SetValue(PropertyInfo property, IEnumerator<string> enumerator) {

            var type = property.PropertyType;

            if ( false ) {
            } else if ( type.Equals( typeof( bool ) ) ) {

                property.SetValue( obj, true );

            } else if ( type.Equals( typeof( string ) ) ) {

                if ( enumerator.MoveNext() ) {

                    property.SetValue( obj, enumerator.Current );

                } else {

                    throw new ArgumentException( "追加情報不足" );

                }

            } else {

                throw new NotSupportedException( $"{type}には、対応していません" );

            }

        }
    }

}
