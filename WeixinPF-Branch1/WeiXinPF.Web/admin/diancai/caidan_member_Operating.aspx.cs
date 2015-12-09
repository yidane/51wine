﻿using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class caidan_member_Operating : Web.UI.ManagePage
    {
        BLL.wx_diancai_blacklist blaqckbll = new BLL.wx_diancai_blacklist();
        Model.wx_diancai_blacklist blaqck = new Model.wx_diancai_blacklist();
        public   string openid = "";
        public  int id = 0;
        public  int status = 0;
        public  string blackName = "";
        public  int shopid = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            openid = MyCommFun.QueryString("openid");
            id = MyCommFun.RequestInt("id");
            status = MyCommFun.RequestInt("status");
            blackName = MyCommFun.QueryString("blackName");
            shopid = MyCommFun.RequestInt("shopid");

        }

        protected void save_status_Click(object sender, EventArgs e)
        {

            int  status =MyCommFun.Str2Int(this.type.Value.ToString());
            blaqckbll.UpdateStatus(id, status);


            if (this.type.Value=="0")
            {
                blaqck.blackName = blackName;
                blaqck.openid = openid;
                blaqck.shopid = shopid;
                blaqck.blackDate = DateTime.Now;
                blaqck.createDate = DateTime.Now;
                blaqckbll.Add(blaqck);

            }
            if (this.type.Value == "1")
            {
                blaqckbll.Delete(openid);
            }


            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "编辑黑名单状态，id为："+id); //记录日志
            JscriptMsg("操作成功", Utils.CombUrlTxt("caidan_member_manage.aspx?shopid=" + shopid, "", ""), "Success");

        }
    }
}