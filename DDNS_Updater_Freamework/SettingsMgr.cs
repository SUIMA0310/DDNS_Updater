using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DDNS_Updater {

    public class SettingsMgr : SettingsMgr<Settings> {

    }

    public class SettingsMgr<T> where T : class {

        #region static

        private static SettingsMgr<Settings> _Current = null;

        public static SettingsMgr<Settings> Current 
            => _Current ?? ( _Current = new SettingsMgr<Settings>() );

        public static Settings Settings
            => Current.Setting;

        #endregion

        #region properties

        private T _Setting = null;

        public string FileName { get; set; } = null;
        public T Setting => _Setting ?? Load();

        #endregion

        #region constructor

        public SettingsMgr() {

        }

        public SettingsMgr(string fileName) {

            FileName = fileName;

        }

        #endregion

        #region public method

        public void Save(T obj) {

            var usingFileName = FileName ?? Reference.DefaultSettingFileName;

            #region Error Check

            if ( obj == null ) {

                throw new ArgumentNullException( "objがNull" );

            }

            #endregion

            var serializer = new XmlSerializer( typeof( T ) );
            using ( var stream = new FileStream( usingFileName, FileMode.Create ) ) {

                serializer.Serialize( stream, obj );

            }

        }

        public T Load() {

            var usingFileName = FileName ?? Reference.DefaultSettingFileName;

            var serializer = new XmlSerializer( typeof( T ) );
            using ( var stream = new FileStream( usingFileName, FileMode.Open ) ) {

                return _Setting = serializer.Deserialize( stream ) as T;

            }

        }

        #endregion

    }

}
