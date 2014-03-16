using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Weixin.Menu
{
    public class MenuManager : AcessTokenBase
    {
        public HttpResult CreateMenu()
        {
            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", m_Token);
            string postData = Menu.CreateTestMenu();
            var result = Util.HttpPost(posturl, postData);
            return result;
        }

        public HttpResult DeleteMenu()
        {
            string getUrl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", m_Token);
            return Util.HttpGet(getUrl);
        }

        public HttpResult SearchMenu()
        {
            string getUrl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", m_Token);
            return Util.HttpGet(getUrl);
        }
    }
}