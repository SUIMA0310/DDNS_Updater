using System;
using System.Text;

namespace DDNS_Updater {

    class Program {

        static void Main(string[] args) {

            try {
                Console.WriteLine( HttpWebAccess.GetResponseText( SettingsMgr.Settings.Url ) );
            }catch( Exception ex ) {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Error.WriteLine( "例外が発生し処理が中断されました。" );
                Console.Error.WriteLine( $"{ex.Message}" );
            }
        }

    }

}
