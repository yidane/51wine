using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 景点详情
    /// </summary>
    public class wx_travel_scenicDetail
    {
        public int Id { get; set; }
        public int ScenicId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 音频文件
        /// </summary>
        public string Audio { get; set; }

        /// <summary>
        /// 是否自动播放音频
        /// </summary>
        public bool? AutoAudio { get; set; }

        /// <summary>
        /// 是否循环播放音频
        /// </summary>
        public bool? LoopAudio { get; set; }

        /// <summary>
        /// 视频文件
        /// </summary>
        public string Video { get; set; }

        /// <summary>
        /// 是否自动播放视频
        /// </summary>
        public bool? AutoVideo { get; set; }

        /// <summary>
        /// 原文链接
        /// </summary>
        public string OriginalLink { get; set; }

    }
}
