using System;
using System.IO;
using System.Text;
using Cargs;
using Cargs.Attributes;

namespace DDNS_Updater {

    public class Program {

        [PropSwitch( "ip" )]
        public static string IpAddress { get; set; }

        static void Main(string[] args) {

            Console.OutputEncoding = Encoding.UTF8;

            var prog = new Program();
            Analyzer.Analyze( prog, args );

            try {

                string Url = SettingsMgr.Settings.Url
                    .Replace( "{IpAddress}", IpAddress ?? HttpWebAccess.GetIpAddress() );
                Console.WriteLine( Url );
                Console.WriteLine( HttpWebAccess.GetResponseText( Url ) );

            }catch( Exception ex ) {
                
                Console.Error.WriteLine( "例外が発生し処理が中断されました。" );
                Console.Error.WriteLine( $"{ex.Message}" );

            }
        }

        [MethodSwitch( "Template" )]
        public static void Template() {

            if ( File.Exists( SettingsMgr.Current.FileName ) ) {

                Console.Error.WriteLine( "設定ファイルは既に存在しています" );
                Console.Error.Write( "上書きしますか？ [y/n] : " );
                if ( Console.ReadLine().ToLower() != "y" ) {
                    return;
                }

            }

            SettingsMgr.Current.Save( new Settings() );
            Console.Error.WriteLine( "設定ファイルを出力しました" );

            Environment.Exit( 0 );

        }

        [MethodSwitch( '?', "help" )]
        public static void Help() {

            Console.WriteLine( Resource.HelpText );
            Environment.Exit( 0 );

        }

    }

}
