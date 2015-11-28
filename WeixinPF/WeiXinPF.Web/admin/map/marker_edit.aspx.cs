using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.map
{
    public partial class marker_edit : Web.UI.ManagePage
    {
        private string _action = MXEnums.ActionEnum.Add.ToString();
        private int _id = 0;
        private BLL.wx_travel_marker _bll = new BLL.wx_travel_marker();

        private double _top = 0;
        private double _left = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            _action = MyCommFun.QueryString("action");
            _id = MyCommFun.RequestInt("id");
            _top = MyCommFun.RequestDouble("top");
            _left = MyCommFun.RequestDouble("left");

            if (!IsPostBack)
            {
                if (_action == MXEnums.ActionEnum.Edit.ToString() && _id != 0)
                {
                    ShowInfo(_id);
                }
            }
        }

        private void ShowInfo(int id)
        {
            Model.wx_travel_marker model = _bll.GetModel(id);
            if (model != null)
            {
                txtName.Text = model.Name;
                txtRemark.Text = model.Remark;
                txtUrl.Text = model.Url;
                txtDescription.Text = model.Description;

                txtAddress.Text = model.extStr1;
                txtLatXPoint.Text = model.Lat.ToString();
                txtLngYPoint.Text = model.Lng.ToString();

                ClientScript.RegisterStartupScript(GetType(), "message",
                        "<script language='javascript'> $(\"#qqframe\").attr(\"src\", \"../../weixin/map/qqmap/qqmap_getLocation.html?lng=" + model.Lng + "&lat=" + model.Lat + "\");</script>");

                rblRecommend.SelectedValue = model.Recommend.HasValue && model.Recommend.Value ? "1" : "0";
            }
        }

        private bool DoAdd()
        {
            Model.wx_travel_marker model = new Model.wx_travel_marker();

            model.wid = GetWeiXinCode().id;
            model.Name = txtName.Text;
            model.Remark = txtRemark.Text;
            model.Url = txtUrl.Text;
            model.Description = txtDescription.Text;

            model.Top = _top;
            model.Left = _left;

            model.extStr1 = txtAddress.Text;
            model.Lat = Convert.ToDouble(txtLatXPoint.Text);
            model.Lng = Convert.ToDouble(txtLngYPoint.Text);

            model.Recommend = rblRecommend.SelectedValue == "1";

            if (_bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加手绘图景点:" + model.Name); //记录日志
                return true;
            };
            return false;
        }

        private bool DoEdit()
        {
            Model.wx_travel_marker model = _bll.GetModel(_id);

            model.wid = GetWeiXinCode().id;
            model.Remark = txtRemark.Text;
            model.Url = txtUrl.Text;
            model.Description = txtDescription.Text;

            model.extStr1 = txtAddress.Text;

            model.Lat = Convert.ToDouble(txtLatXPoint.Text);
            model.Lng = Convert.ToDouble(txtLngYPoint.Text);

            model.Recommend = rblRecommend.SelectedValue == "1";

            if (_bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "修改手绘图景点:" + model.Name); //记录日志
                return true;
            }
            return false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit())
                {
                    JscriptMsg("修改过程中发生错误啦！", "", "Error");
                    return;
                }
                //JscriptMsg("修改信息成功！", "scenic_list.aspx", "Success");
            }
            else if (_action == MXEnums.ActionEnum.Add.ToString()) //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                //JscriptMsg("添加信息成功！", "scenic_list.aspx", "Success");
            }
        }
    }
}