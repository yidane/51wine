using System;

namespace WeiXinPF.Application.DomainModules.Message.Dtos
{
    public  class MessageDto
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string CreateTime { get; set; }
        public bool IsRead { get; set; }

        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
       

        public string FromUserName { get; set; } 
    }
}
