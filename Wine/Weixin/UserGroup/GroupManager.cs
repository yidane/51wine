using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weixin.UserGroup
{
    public class GroupManager : AcessTokenBase
    {
        public Group Create(string groupName)
        {
            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", m_Token);
            var result = Util.HttpPost(postUrl, groupName);
            if (result.Flag)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Group>(result.Result);
            }
            return null;
        }
    }
}