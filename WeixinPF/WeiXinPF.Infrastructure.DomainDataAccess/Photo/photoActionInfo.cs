﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Photo
{
    [Table("wx_travel_photoActionInfo")]
    public partial  class photoActionInfo
    {
        #region Properties
        public int id { get; set; }
        public int wid { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate    { get; set; }
        public string brief { get; set; }
        public string actContent { get; set; }
        public bool isAllowSharing { get; set; }

            

        #endregion
    }

    public partial class photoActionInfo
    {
        #region Method
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public static IQueryable<photoActionInfo> GetList(int wid)
        {
            IQueryable<photoActionInfo> result = null;
            var db=new WXDBContext();
            result = db.photoActionInfo.Where(p => p.wid == wid);

            return result;
        }



        #endregion

        public void Add()
        {
            var db = new WXDBContext();

        }
    }
}