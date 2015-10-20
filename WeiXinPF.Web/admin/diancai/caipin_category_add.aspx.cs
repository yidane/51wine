﻿using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class caipin_category_add : Web.UI.ManagePage
    {
        protected  int shopid = 0;
        public  string type = "";
        public  int ids = 0;
        BLL.wx_diancai_caipin_category caipinbll = new BLL.wx_diancai_caipin_category();
        Model.wx_diancai_caipin_category caipin = new Model.wx_diancai_caipin_category();
        protected void Page_Load(object sender, EventArgs e)
        {
            shopid = MyCommFun.RequestInt("shopid");
            type = MyCommFun.QueryString("type");
            ids = MyCommFun.RequestInt("id");
            if (!Page.IsPostBack)
            {
             
                if (type == "edite")//修改
                {
                    caipin = caipinbll.GetModel(ids);
                    this.sortid.Text = caipin.sortid.ToString();
                    this.categoryName.Text = caipin.categoryName;
                    this.miaoshu.Text = caipin.miaoshu;
                    this.isStart.SelectedValue = caipin.isStart.ToString();

                }
                
            }
        }

        protected void save_caidan_Click(object sender, EventArgs e)
        {

          if(type=="add")
          {
             
            caipin.shopid = shopid;
            caipin.sortid =MyCommFun.Str2Int( this.sortid.Text);
            caipin.categoryName = this.categoryName.Text;
            caipin.miaoshu = this.miaoshu.Text;
            caipin.isStart = Convert.ToBoolean( this.isStart.SelectedValue);
            caipin.createDate = DateTime.Now;
            int id = caipinbll.Add(caipin);
            AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加商品分类，主键为" + id); //记录日志
            JscriptMsg("增加成功！", Utils.CombUrlTxt("caipin_category.aspx?shopid='" + shopid + "'&manage=managetype", "keywords={0}", ""), "Success");
           }

           if (type == "edite")
            {
                shopid = MyCommFun.RequestInt("shopid");
                caipin = caipinbll.GetModel(ids);
             
                caipin.shopid = shopid;
                caipin.sortid = Convert.ToInt32( this.sortid.Text);
                caipin.categoryName = this.categoryName.Text;
                caipin.miaoshu = this.miaoshu.Text;
                caipin.isStart = Convert.ToBoolean( this.isStart.SelectedValue);
                caipin.createDate = DateTime.Now;
                caipinbll.Update(caipin);
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改商品分类，主键为" + ids); //记录日志
                JscriptMsg("修改成功！", Utils.CombUrlTxt("caipin_category.aspx?shopid='" + shopid + "'&manage=managetype", "keywords={0}", ""), "Success");
         }

        }
    }
}