using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.scenic
{
    public partial class scenic_edit : Web.UI.ManagePage
    {
        private string _action = MXEnums.ActionEnum.Add.ToString();
        private int _id = 0;
        private BLL.wx_travel_scenic _bll = new BLL.wx_travel_scenic();
        protected void Page_Load(object sender, EventArgs e)
        {
            _action = MXRequest.GetQueryString("action");
            _id = MXRequest.GetQueryInt("Id");
            if (!IsPostBack)
            {
                if (_action == MXEnums.ActionEnum.Edit.ToString() && _id > 0)
                {
                    ShowInfo(_id);
                }
            }
        }

        #region 增加操作
        private bool DoAdd()
        {
            bool result = false;
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_travel_scenic model = new Model.wx_travel_scenic
            {
                wid = weixin.id,
                Name = txtName.Text.Trim(),
                Description = txtDescription.InnerText.Trim(),
                TemplateId = 1,
                FirstBgImg = txtFirstBgImg.Text,
                IdentifyImg = txtIdentifyImg.Text,
                DisplayAction = rblDiaplayAction.SelectedItem.Value,
                SecondBgImg = txtSecondBgImg.Text.Trim(),
                AutoDisplayNextPage = rblAutoDisplayNextPage.SelectedItem.Value == "1",
                Delay = Convert.ToInt32(txtDelay.Text)
            };


            if (_bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "微旅游添加景区导览:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 编辑操作

        private bool DoEdit(int id)
        {
            bool result = false;
            Model.wx_travel_scenic model = _bll.GetModel(id);
            model.Name = txtName.Text.Trim();
            model.Description = txtDescription.InnerText.Trim();
            model.TemplateId = 1;
            model.FirstBgImg = txtFirstBgImg.Text;
            model.IdentifyImg = txtIdentifyImg.Text;
            model.DisplayAction = rblDiaplayAction.SelectedItem.Value;
            model.SecondBgImg = txtSecondBgImg.Text.Trim();
            model.AutoDisplayNextPage = rblAutoDisplayNextPage.SelectedItem.Value == "1";
            model.Delay = MyCommFun.Str2Int(txtDelay.Text);

            if (_bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "微旅游编辑景区导览:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }

        #endregion
        private void ShowInfo(int id)
        {
            Model.wx_travel_scenic model = _bll.GetModel(id);
            model.Name = txtName.Text = model.Name;
            txtDescription.InnerText = model.Description;
            txtFirstBgImg.Text = model.FirstBgImg;
            txtIdentifyImg.Text = model.IdentifyImg;
            rblDiaplayAction.SelectedValue = model.DisplayAction;
            txtSecondBgImg.Text = model.SecondBgImg;
            rblAutoDisplayNextPage.SelectedValue = model.AutoDisplayNextPage.HasValue && model.AutoDisplayNextPage.Value ? "1" : "0";
            txtDelay.Text = model.Delay.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("scenic", MXEnums.ActionEnum.Edit.ToString());//检查权限
                if (!DoEdit(this._id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "scenic_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("scenic", MXEnums.ActionEnum.Add.ToString());//检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "scenic_list.aspx", "Success");
            }
        }
    }
}