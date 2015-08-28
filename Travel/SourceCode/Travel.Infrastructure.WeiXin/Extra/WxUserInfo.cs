using System.Collections.Generic;

namespace Travel.Infrastructure.WeiXin.Extra
{
    /// <summary>
    /// 粉丝个人信息明细
    /// </summary>
    public class WxUserInfo
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string FakeId { get; set; }

        public int GroupID { get; set; }

        public List<WxUserGroup> Groups { get; set; }

        public string NickName { get; set; }

        public string Province { get; set; }

        public string ReMarkName { get; set; }

        /// <summary>
        /// 1：男; 2:女
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 个人签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 微信登陆名
        /// </summary>
        public string Username { get; set; }

        #region 额外
        public string OpenId { get; set; }
        #endregion

    }
    public class WxUserGroup
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
