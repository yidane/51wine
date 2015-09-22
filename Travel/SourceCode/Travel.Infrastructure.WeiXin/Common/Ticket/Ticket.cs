using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using OneGulp.WeChat;
using Travel.Infrastructure.WeiXin.Config;

namespace Travel.Infrastructure.WeiXin.Common.Ticket
{
    public abstract class Ticket
    {
        protected readonly static Dictionary<string, string> MultiTicketCache = new Dictionary<string, string>();

        /// <summary>
        /// 票据类型
        /// </summary>
        protected abstract string type { get; }

        protected Credential m_Credential = null;

        protected Ticket(Credential credential)
        {
            if (credential == null)
                throw new ArgumentNullException("credential", "credential不能为空");

            m_Credential = credential;
        }

        /// <summary>
        /// 生成Http请求对象
        /// </summary>
        /// <returns></returns>
        protected HttpHelper GetHelper()
        {
            var ret = new HttpHelper(WeChatUrlConfigManager.SystemManager.JsAPIUrl);
            return ret;
        }

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        public string CreateTimeStamp()
        {
            return ((int)(DateTime.Now - DateTime.Parse("1970-1-1")).TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public string CreateRandomString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="sha1Before"></param>
        /// <returns></returns>
        protected static string SHA1(string sha1Before)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]
            var enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(sha1Before);

            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash.ToLower();
        }

        /// <summary>
        /// 生成票据
        /// </summary>
        /// <returns></returns>
        public string CreateTicket()
        {
            //if (MultiTicketCache.ContainsKey(m_Credential.WeChatAccountManager.AppId))
            //    return MultiTicketCache[m_Credential.WeChatAccountManager.AppId];

            //var helper = GetHelper();
            //var ret = helper.Get<TicketResult>(new FormData
            //        {
            //            {"type", type},
            //            {"access_token", m_Credential.AccessToken}
            //        });

            //if (!ret.IsSucceed)
            //    return string.Empty;

            //MultiTicketCache[m_Credential.WeChatAccountManager.AppId] = ret.ticket; //缓存
            //var autoEvent = new AutoResetEvent(false);
            //var timer = new Timer(state => autoEvent.Set(), autoEvent, (ret.expires_in - 3) /*避免时间误差*/* 1000, Timeout.Infinite);
            //timer.Dispose(autoEvent);

            //return MultiTicketCache[m_Credential.WeChatAccountManager.AppId];

            var ticket = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetTicket(m_Credential.WeChatAccountManager.AppId,
                                                                    m_Credential.WeChatAccountManager.AppSecret);
            if (ticket != null && ticket.errcode == ReturnCode.请求成功)
                return ticket.ticket;

            return string.Empty;
        }
    }

    public class TicketResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }

        public bool IsSucceed
        {
            get { return !string.IsNullOrEmpty(ticket); }
        }
    }
}
