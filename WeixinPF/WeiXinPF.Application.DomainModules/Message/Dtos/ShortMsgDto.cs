using System;
using WeiXinPF.Application.DomainModules.User.DTOS;

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
        public string CreateTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsShowButton { get; set; }

        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }

        public int FromUserId { get; set; }
        public int ToUserId { get; set; } 

        public virtual UserDto FromUser { get; set; }
        public virtual UserDto ToUser { get; set; }
    }
}
