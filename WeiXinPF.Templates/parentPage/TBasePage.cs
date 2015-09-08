﻿/**************************************
 * 
 * author:李朴
 * company:上海沐雪网络科技有限公司
 * website:http://uweixin.cn
 * createDate:2013-11-1
 * update:2014-12-30
 * remark:微网站页面的父类
 * 
 ***********************************/

using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeiXinPF.Templates
{
    public class TBasePage : System.Web.UI.Page
    {
        
        /// <summary>
        /// 模版的物理路径
        /// </summary>
        public string tPath { get; set; }
        
        /// <summary>
        /// 首页的模版文件名称
        /// </summary>
        public string templateIndexFileName { get; set; }
         
        /// <summary>
        /// 列表页面的模版文件名称
        /// </summary>
        public string templateListFileName { get; set; }
        

        /// <summary>
        /// 详情页面的模版文件名称
        /// </summary>
        public string templateDetailName { get; set; }
         
        /// <summary>
        /// 微帐号wid
        /// </summary>
        public int wid { get; set; }
         
        /// <summary>
        /// 初始化模版的错误信息
        /// </summary>
        public string errInitTemplates { get; set; }

        public TBasePage()
        {
            tPath = "";
            templateIndexFileName = "";
            templateListFileName = "";
            templateDetailName = "";
            errInitTemplates = "";

             wid =  MyCommFun.RequestInt("wid");
             BLL.wx_userweixin wuwBll = new BLL.wx_userweixin();
             if (wid == 0)
             {
                 errInitTemplates = "链接地址或者参数错误！";
                 return;
             }

             bool isExist = wuwBll.wxCodeLegal(wid);
             if (!isExist)
             {
                 errInitTemplates = "账号已过期或已被禁用！";
                 return;
             
             }
          

          
        }
    }
}
