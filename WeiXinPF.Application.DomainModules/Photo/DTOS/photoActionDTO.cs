using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Photo.DTOS
{
   public  class photoActionDTO
    {
        public int id { get; set; }
        public int wid { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string brief { get; set; }
        public string actContent { get; set; }
        public bool isAllowSharing { get; set; }
    }
}
