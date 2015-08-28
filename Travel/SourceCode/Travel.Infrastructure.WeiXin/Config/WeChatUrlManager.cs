namespace Travel.Infrastructure.WeiXin.Config
{
    public static class WeChatUrlConfigManager
    {
        /// <summary>
        /// 微信系统设置
        /// </summary>
        public static class SystemManager
        {
            /// <summary>
            /// 微信Token
            /// </summary>
            public const string TokenUrl = "https://api.weixin.qq.com/cgi-bin/token";

            /// <summary>
            /// 获取页面访问OpenId
            /// </summary>
            public const string Access_TokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";

            /// <summary>
            /// 通过jsapi获取jsapi_ticket
            /// </summary>
            public const string JsAPIUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";
        }

        /// <summary>
        /// 菜单管理
        /// </summary>
        public static class MenuManager
        {
            /// <summary>
            /// 创建菜单
            /// </summary>
            public static string CreateUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/create";

            /// <summary>
            /// 查询菜单接口地址
            /// </summary>
            public static string QueryUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/get";

            /// <summary>
            /// 删除菜单接口地址
            /// </summary>
            public static string DeleteUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/delete";
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        public static class UserManager
        {
            /// <summary>
            /// 创建分组
            /// </summary>
            public const string CreateGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/create";

            /// <summary>
            /// 删除用户分组
            /// </summary>
            public const string DeleteGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/delete";

            /// <summary>
            /// 获取所有分组
            /// </summary>
            public const string GetGroupListUrl = "https://api.weixin.qq.com/cgi-bin/groups/get";

            /// <summary>
            /// 获取用户所在分组
            /// </summary>
            public const string GetGroupByOpenIDUrl = "https://api.weixin.qq.com/cgi-bin/groups/getid";

            /// <summary>
            /// 修改分组名称
            /// </summary>
            public const string UpdateGroupInfoUrl = "https://api.weixin.qq.com/cgi-bin/groups/update";

            /// <summary>
            /// 修改用户分组
            /// </summary>
            public const string UpdateUserGroupInfoUrl = "https://api.weixin.qq.com/cgi-bin/groups/members/update";

            /// <summary>
            /// 批量移动用户分组
            /// </summary>
            public const string UpdateUserListGroupInfoUrl = "https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate";

            /// <summary>
            /// 获取用户基本信息
            /// </summary>
            public const string GetUserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info";
        }

        /// <summary>
        /// 消息管理
        /// </summary>
        public static class MessageManager
        {

        }

        /// <summary>
        /// 素材管理
        /// </summary>
        public static class ResourceManager
        {
            /// <summary>
            /// 上传素材
            /// </summary>
            public const string UploadResourceUrl = "https://api.weixin.qq.com/cgi-bin/media/upload";

            /// <summary>
            /// 获取临时素材
            /// </summary>
            public const string GetResourceUrl = "https://api.weixin.qq.com/cgi-bin/media/get";
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        public const string DefaultUserListUrl = "https://mp.weixin.qq.com/cgi-bin/contactmanage";
        public const string DefaultLoginReferUrl = "https://mp.weixin.qq.com/";
        public const string DefaultLoginUrl = "http://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN";
        public const string DefaultUserInfoUrl = "https://mp.weixin.qq.com/cgi-bin/getcontactinfo";
    }
}
