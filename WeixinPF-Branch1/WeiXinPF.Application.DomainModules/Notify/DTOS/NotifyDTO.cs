using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Notify.DTOS
{
   public  class NotifyDTO
    {
        public string type { get; set; }
        public string content { get; set; }
        public string notifyUrl { get; set; }
    }
}
