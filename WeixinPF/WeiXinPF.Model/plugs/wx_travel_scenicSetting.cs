using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 景区导览模版设置
    /// </summary>
    public class wx_travel_scenicSetting
    {
        public int Id { get; set; }
        public int ScenicId { get; set; }
        public string TemplateName { get; set; }
        public string BackgroundImage { get; set; }
        public string Audio { get; set; }
        public bool? AutoAudio { get; set; }
        public bool? LoopAudio { get; set; }
        public string Video { get; set; }
        public bool? AutoVideo { get; set; }
    }
}
