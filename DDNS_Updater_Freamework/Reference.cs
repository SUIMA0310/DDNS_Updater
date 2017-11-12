using System;
using System.Collections.Generic;
using System.Text;

namespace DDNS_Updater {

    public static class Reference {

        #region HttpWebAccess

        /// <summary>
        /// IPAddress取得用の外部システム
        /// </summary>
        public static string GetIpUrl = "https://dyn.value-domain.com/cgi-bin/dyn.fcg?ip";

        #endregion

        #region Settings

        /// <summary>
        /// DDNSに新たなIPAddressを登録するためのURL
        /// </summary>
        public static string DefaulUrl = "https://dyn.value-domain.com/cgi-bin/dyn.fcg?d=ドメイン名&p=パスワード&h=ホスト名&i={IpAddress}";

        #endregion

        #region SettingsMgr

        public static string DefaultSettingFileName = "Settings.xml";

        public static Type DefaultSettingType = typeof(Settings);

        #endregion

    }

}
