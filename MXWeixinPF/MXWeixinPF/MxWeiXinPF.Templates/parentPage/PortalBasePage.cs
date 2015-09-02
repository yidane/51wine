/**************************************
 * 1e2124dd04e11d01b9df2865f85944be
 * author:李朴
 * company:上海沐雪网络科技有限公司
 * website:http://uweixin.cn
 * createDate:2013-11-1
 * update:2014-12-30
 * remark:门户网站页面的父类
 * 
 ***********************************/

using MxWeiXinPF.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MxWeiXinPF.Templates
{
    public class PortalBasePage : System.Web.UI.Page
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
        /// 初始化模版的错误信息
        /// </summary>
        public string errInitTemplates { get; set; }

        protected internal Model.siteconfig config = new BLL.siteconfig().loadConfig();
        protected internal Model.userconfig uconfig = new BLL.userconfig().loadConfig();


        public PortalBasePage()
        {
            tPath = "";
            templateIndexFileName = "";
            templateListFileName = "";
            templateDetailName = "";
            errInitTemplates = "";

            if (config.webstatus == 0)
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode(config.webclosereason));
                return;
            }
          
        }

      
    }
}
