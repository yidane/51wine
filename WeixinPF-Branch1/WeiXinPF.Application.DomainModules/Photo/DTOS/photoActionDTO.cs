using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Photo.DTOS
{
    /// <summary>
    /// 湖怪的dto
    /// </summary>
   public partial  class photoActionDTO
    {
        public int id { get; set; }
        public int wid { get; set; }
        public string actName { get; set; }
        public string createTime { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string brief { get; set; }
        public string actContent { get; set; }
        public bool isAllowSharing { get; set; }
    }

    /// <summary>
    /// 自定义的属性
    /// </summary>
    public partial class photoActionDTO
    {
        public string status_s { get; set; }
        public string url { get; set; }
    }
}
