namespace Weixin
{
    public class AcessTokenBase
    {
        protected string m_AppId = string.Empty;
        protected string m_Token = string.Empty;

        #region AccessToken Fields
        private static int ExpridTime = 720;
        #endregion


        public AcessTokenBase()
        {
            this.m_AppId = System.Configuration.ConfigurationManager.AppSettings["AppID"];
            this.m_Token = System.Configuration.ConfigurationManager.AppSettings["ACCESS_TOKEN"];
        }

        public AcessTokenBase(string appId, string token)
        {
            this.m_AppId = appId;
            this.m_Token = token;
        }
    }
}