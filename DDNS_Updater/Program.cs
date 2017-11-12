using System;
using System.IO;
using System.Text;
using ConsoleArguments.Attributes;

namespace DDNS_Updater {

    class Program {

        [Argument( 't', "test")]
        public bool Test { get; set; }

        [Argument( 'a' )]
        public string Test2 { get; set; }

        [Argument( "Template" )]
        public void Template() {

            if( !File.Exists( SettingsMgr.Current.FileName ) ) {

                SettingsMgr.Current.Save( new Settings() );
                Console.Error.WriteLine( "設定ファイルを出力しました" );

            } else {

                Console.Error.WriteLine( "設定ファイルは既に存在しています" );

            }

            Environment.Exit(0);

        }

        static void Main(string[] args) {

            Console.OutputEncoding = Encoding.UTF8;

            var prog = new Program();
            ConsoleArguments.Analyzer.Analyze( prog, args );

            try {

                var Url = SettingsMgr.Settings.Url
                    .Replace( "{IpAddress}", HttpWebAccess.GetIpAddress() );
                Console.WriteLine( Url );
                Console.WriteLine( HttpWebAccess.GetResponseText( Url ) );

            }catch( Exception ex ) {
                
                Console.Error.WriteLine( "例外が発生し処理が中断されました。" );
                Console.Error.WriteLine( $"{ex.Message}" );

            }
        }

    }

}
