using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using static DDNS_Updater.Reference;

namespace DDNS_Updater {
    public class HttpWebAccess {

        /// <summary>
        /// 自身のグローバルIPAddressを取得する。
        /// </summary>
        /// <returns>xxx.xxx.xxx.xxx</returns>
        public static string GetIpAddress() {

            return GetResponseTextAsync( GetIpUrl ).Result;

        }

        /// <summary>
        /// 自身のグローバルIPAddressを非同期で取得する。
        /// </summary>
        /// <returns>xxx.xxx.xxx.xxx</returns>
        public static Task<string> GetIpAddressAsync() {

            return GetResponseTextAsync( GetIpUrl );

        }

        /// <summary>
        /// Http Getした結果をテキストで取得する。
        /// </summary>
        /// <param name="url">アクセスするURL</param>
        /// <returns>response</returns>
        public static string GetResponseText( string url ) {

            return GetResponseTextAsync( WebRequest.CreateHttp(url) ).Result;

        }

        /// <summary>
        /// Http Getした結果をテキストで非同期に取得する。
        /// </summary>
        /// <param name="url">アクセスするURL</param>
        /// <returns>response</returns>
        public static Task<string> GetResponseTextAsync(string url) {

            return GetResponseTextAsync( WebRequest.CreateHttp( url ) );

        }

        protected static async Task<string> GetResponseTextAsync( WebRequest request) {

            using ( var response = await request.GetResponseAsync().ConfigureAwait( false ) )
            using ( var stream = response.GetResponseStream() )
            using ( var reader = new StreamReader( stream ) ) {

                return await reader.ReadToEndAsync().ConfigureAwait( false );

            }

        }

    }
}
