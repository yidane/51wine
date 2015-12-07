using System;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.Model.message;

namespace WeiXinPF.Application.DomainModules.Message.Dtos
{
    /// <summary>
    /// 短消息
    /// </summary> 
    public class ShortMsgDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string MenuType { get; set; }

        public string CreateTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsShowButton { get; set; }

        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonMutipleUrl { get; set; }

        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        public MsgUserType MsgToUserType { get; set; }
        public MsgUserType MsgFromUserType { get; set; }

        public virtual UserManagerDto FromUser { get; set; }
        public virtual UserManagerDto ToUser { get; set; }
    }
   
}
