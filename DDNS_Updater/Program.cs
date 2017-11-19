using System;
using System.IO;
using System.Text;
using Cargs;
using Cargs.Attributes;

namespace DDNS_Updater {

    public class Program {

        [PropSwitch( "ip" , Option = SwitchOptions.String)]
        public static string IpAddress { get; set; }

        [PropSwitch( "url", Option = SwitchOptions.String )]
        public static string Url { get; set; }

        static void Main(string[] args) {

            Console.OutputEncoding = Encoding.UTF8;

            var prog = new Program();
            Analyzer.Analyze( prog, args );

            try {

                string url = Url ?? SettingsMgr.Settings.Url;
                url = url.Replace( "{IpAddress}", IpAddress ?? HttpWebAccess.GetIpAddress() );
                Console.WriteLine( url );
                Console.WriteLine( HttpWebAccess.GetResponseText( url ) );

            }catch( Exception ex ) {
                
                Console.Error.WriteLine( "例外が発生し処理が中断されました。" );
                Console.Error.WriteLine( $"{ex.Message}" );

            }
        }

        [MethodSwitch( "Settings", SwitchOptions.String )]
        public static void SettingPath( string path ) {

            SettingsMgr.Current.FileName = path;

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
