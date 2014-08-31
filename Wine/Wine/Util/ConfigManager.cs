using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Wine.Util
{
    public class ConfigManager
    {
        private static string m_DBConnection = string.Empty;

        public static string DBConnection
        {
            get
            {

#if DEBUG
                return "server=localhost; user id=sa; password=sasasasasa; Database=WineShop";
#endif

                m_DBConnection = string.IsNullOrEmpty(m_DBConnection) ? System.Configuration.ConfigurationManager.AppSettings["DBConnection"] : m_DBConnection;

                return m_DBConnection;
            }
        }

        public static string WapSiteUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["WapSiteUrl"]; }
        }
    }
}