using System;
using System.Net.Http;

namespace DDNS_Updater {

    class Program {

        static void Main(string[] args) {

            Console.WriteLine( HttpWebAccess.GetResponseText( SettingsMgr.Settings.Url ) );

        }

    }

}
