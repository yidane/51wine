using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 景区导览
    /// </summary>
    public class wx_travel_scenic
    {
        public int Id { get; set; }
        public int wid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TemplateId { get; set; }
        /// <summary>
        /// 首页背景图片
        /// </summary>
        public string FirstBgImg { get; set; }
        /// <summary>
        /// 景区标识图片
        /// </summary>
        public string IdentifyImg { get; set; }
        /// <summary>
        /// 景区标识显示动画
        /// </summary>
        public string DisplayAction { get; set; }

        /// <summary>
        /// 第二页背景图片
        /// </summary>
        public string SecondBgImg { get; set; }
        /// <summary>
        /// 是否自动显示第二页
        /// </summary>
        public bool? AutoDisplayNextPage { get; set; }

        /// <summary>
        /// 第二页自动显示延迟时间
        /// </summary>
        public int? Delay { get; set; }

        /// <summary>
        /// 音频文件
        /// </summary>
        public string AudioPath { get; set; }
        /// <summary>
        /// 是否自动播放音频文件
        /// </summary>
        public bool? AutoPlayAudio { get; set; }

        /// <summary>
        /// 是否循环播放音频文件
        /// </summary>
        public bool? AudioLoop { get; set; }

        /// <summary>
        /// 视频文件
        /// </summary>
        public string VideoPath { get; set; }

        /// <summary>
        /// 是否自动播放视频文件
        /// </summary>
        public bool? AutoPlayVideo { get; set; }
        public int? extInt1 { get; set; }
        public int? extInt2 { get; set; }
        public string extStr1 { get; set; }
        public string extStr2 { get; set; }
    }
}
