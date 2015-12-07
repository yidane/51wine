using System.Collections.Generic;

namespace WeiXinPF.Application.DomainModules.Message.Dtos
{
    public class ListMsgWithCount
    {
        public List<ShortMsgWithCountDto> Msgs { get; set; }
        public int Count { get; set; }
    }
}
