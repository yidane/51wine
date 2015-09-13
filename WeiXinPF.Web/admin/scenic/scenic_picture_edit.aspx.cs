using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.scenic
{
    public partial class scenic_picture_edit : Web.UI.ManagePage
    {
        private string _action = MXEnums.ActionEnum.Add.ToString(); //操作类型

        BLL.wx_travel_picture _bll = new BLL.wx_travel_picture();
        protected int DetailId;
        protected int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _action = MXRequest.GetQueryString("action");
            DetailId = MyCommFun.RequestInt("detailId");
            Id = MXRequest.GetQueryInt("Id");
            if (!Page.IsPostBack)
            {
                if (_action == MXEnums.ActionEnum.Edit.ToString() && Id > 0) //修改
                {
                    ShowInfo(Id);
                }
            }
        }



        #region 赋值操作=================================
        private void ShowInfo(int id)
        {
            hidid.Value = id.ToString();
            Model.wx_travel_picture model = _bll.GetModel(id);
            txtName.Text = model.Name;
            txtContent.Value = model.Content;
            txtOrderNo.Text = model.OrderNo.ToString();

            //封面图片
            if (model.PicPath != null && model.PicPath.Trim() != "/images/noneimg.jpg")
            {
                txtImgUrl.Text = model.PicPath;
                imgfacePicPic.ImageUrl = model.PicPath;
            }
        }

        #endregion

        #region 增加操作
        private bool DoAdd()
        {
            bool result = false;
            Model.wx_travel_picture model = new Model.wx_travel_picture
            {
                DetailId = DetailId,
                Name = txtName.Text.Trim(),
                PicPath = txtImgUrl.Text.Trim(),
                Content = txtContent.InnerText,
                OrderNo = MyCommFun.Str2Int(txtOrderNo.Text.Trim()),
                CreateDate = DateTime.Now
            };


            if (_bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "微旅游添加景点图片:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 编辑操作

        private bool DoEdit(int id)
        {
            bool result = false;
            Model.wx_travel_picture model = _bll.GetModel(id);
            model.DetailId = DetailId;
            model.Name = txtName.Text.Trim();
            model.PicPath = txtImgUrl.Text.Trim();
            model.Content = txtContent.InnerText;
            model.OrderNo = MyCommFun.Str2Int(txtOrderNo.Text.Trim());
            model.CreateDate = DateTime.Now;

            if (_bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "微旅游编辑景点图片:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }

        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(Id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", Utils.CombUrlTxt("scenic_picture_list.aspx", "detailId={0}", DetailId.ToString()), "Success");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", Utils.CombUrlTxt("scenic_picture_list.aspx", "detailId={0}", DetailId.ToString()), "Success");
            }
        }
    }
}