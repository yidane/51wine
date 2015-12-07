using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.scenic
{
    public partial class scenic_detail_edit : Web.UI.ManagePage
    {
        private string _action = MXEnums.ActionEnum.Add.ToString();
        private int _id = 0;
        protected int ScenicId = 0;
        private readonly BLL.wx_travel_scenicDetail _bll = new BLL.wx_travel_scenicDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            _action = MXRequest.GetQueryString("action");
            _id = MXRequest.GetQueryInt("Id");
            ScenicId = MXRequest.GetQueryInt("scenicId");
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
            Model.wx_travel_scenicDetail model = new Model.wx_travel_scenicDetail
            {
                ScenicId = ScenicId,
                Name = txtName.Text.Trim(),
                Cover = txtCover.Text.Trim(),
                BackgroundImage = txtBackgroundImage.Text.Trim(),
                Digest = txtDigest.Text.Trim(),
                Content = txtContent.InnerText.Trim(),
                Audio = txtAudio.Text,
                AutoAudio = rblAutoAudio.SelectedItem.Value == "1",
                LoopAudio = rblLoopAudio.SelectedItem.Value == "1"
            };


            if (_bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "微旅游添加景点详情:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 编辑操作

        private bool DoEdit(int id)
        {
            bool result = false;
            Model.wx_travel_scenicDetail model = _bll.GetModel(id);
            model.Name = txtName.Text.Trim();
            model.Cover = txtCover.Text.Trim();
            model.BackgroundImage = txtBackgroundImage.Text.Trim();
            model.Digest = txtDigest.Text.Trim();
            model.Content = txtContent.InnerText.Trim();
            model.Audio = txtAudio.Text;
            model.AutoAudio = rblAutoAudio.SelectedItem.Value == "1";
            model.LoopAudio = rblLoopAudio.SelectedItem.Value == "1";

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
            Model.wx_travel_scenicDetail model = _bll.GetModel(id);
            txtName.Text = model.Name;
            txtCover.Text = model.Cover;
            txtBackgroundImage.Text = model.BackgroundImage;
            txtDigest.Text = model.Digest;
            txtContent.InnerText = model.Content;
            txtAudio.Text = model.Audio;
            rblAutoAudio.SelectedValue = model.AutoAudio.HasValue && model.AutoAudio.Value ? "1" : "0";
            rblLoopAudio.SelectedValue = model.LoopAudio.HasValue && model.LoopAudio.Value ? "1" : "0";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(this._id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", Utils.CombUrlTxt("scenic_detail_list.aspx", "scenicId={0}", ScenicId.ToString()), "Success");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", Utils.CombUrlTxt("scenic_detail_list.aspx", "scenicId={0}", ScenicId.ToString()), "Success");
            }
        }
    }
}