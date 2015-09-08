using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
     public class WXLogs
    {

        #region  不带wid的日志记录
        public static void AddLog(string content)
        {
            AddLog("", "", content);
        }
        public static void AddErrLog(string content)
        {
            AddLog("", "", content, 0);
        }
        public static void AddLog(string modelName, string funName, string content)
        {
            AddLog(modelName, funName, content, 1);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志内容</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        public static void AddLog(string modelName, string funName, string content, int logsType)
        {

             AddLog(modelName, funName, content, logsType, "");
        }

        /// <summary>
        /// 添加flg标志
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="funName"></param>
        /// <param name="flg"></param>
        public static void AddFlg(string modelName, string funName, string flg)
        {
            AddLog(modelName, funName, "", 1, flg, "");
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志名称</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        /// <param name="flg">标志</param>
        public static void AddLog(string modelName, string funName, string content, int logsType, string flg)
        {
             AddLog(modelName, funName, content, logsType, flg, "");
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志名称</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        /// <param name="flg">标志</param>
        /// <param name="flg2">标志2</param>
        public static void AddLog(string modelName, string funName, string content, int logsType, string flg, string flg2)
        {
             WeiXinPF.DAL.wx_logs dal=new WeiXinPF.DAL.wx_logs();

            Model.wx_logs log = new Model.wx_logs();
            log.modelName = modelName;
            log.funName = funName;
            log.logsContent = content;
            log.logsType = logsType;
            log.flg = flg;
            log.flg2 = flg2;
            log.createDate = DateTime.Now;
            dal.Add(log);
        }

        /// <summary>
        /// 是否存在标志flg
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        public static bool ExistsFlg(string flg)
        {
            WeiXinPF.DAL.wx_logs dal = new WeiXinPF.DAL.wx_logs();
            return dal.ExistsFlg(flg);
        }

        #endregion  ExtensionMethod

        #region  带wid的日志记录

        public static  void AddFlg(int wid, string modelName, string funName, string flg)
        {
            AddLog(wid, modelName, funName, "", 1, flg, "");
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="content"></param>
        public static void AddLog(int wid, string content)
        {
            AddLog(wid, "", "", content);
        }

        /// <summary>
        /// 添加日志：错误的
        /// </summary>
        /// <param name="wid">微帐号</param>
        /// <param name="content">日志内容</param>
        public static void AddErrLog(int wid, string content)
        {
            AddErrLog(wid, "", "", content);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="wid">微帐号</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志内容</param>
        public static void AddLog(int wid, string modelName, string funName, string content)
        {
            AddLog(wid, modelName, funName, content, 1);
        }

        /// <summary>
        /// 添加日志：错误的
        /// </summary>
        /// <param name="wid">微帐号</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志内容</param>
        public static void AddErrLog(int wid, string modelName, string funName, string content)
        {
            AddLog(wid, modelName, funName, content, 0);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="wid">微帐号</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志内容</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        public static void AddLog(int wid, string modelName, string funName, string content, int logsType)
        {
           AddLog(wid, modelName, funName, content, logsType, "");
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志名称</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        /// <param name="flg">标志</param>
        public static void AddLog(int wid, string modelName, string funName, string content, int logsType, string flg)
        {
            AddLog(wid, modelName, funName, content, logsType, flg, "");
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="funName">方法名称</param>
        /// <param name="content">日志名称</param>
        /// <param name="logsType">日志类型（0错误，1正常）</param>
        /// <param name="flg">标志</param>
        /// <param name="flg2">标志2</param>
        public static void AddLog(int wid, string modelName, string funName, string content, int logsType, string flg, string flg2)
        {
            WeiXinPF.DAL.wx_logs dal = new WeiXinPF.DAL.wx_logs();

            Model.wx_logs log = new Model.wx_logs();
            log.wid = wid;
            log.modelName = modelName;
            log.funName = funName;
            log.logsContent = content;
            log.logsType = logsType;
            log.flg = flg;
            log.flg2 = flg2;
            log.createDate = DateTime.Now;
            dal.Add(log);
        }

        #endregion  ExtensionMethod

    }
}
