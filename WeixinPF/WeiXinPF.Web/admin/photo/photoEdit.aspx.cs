using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.Application.DomainModules.Photo.DTOS;
using WeiXinPF.BLL;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.photo
{

    public partial class photoEdit : Web.UI.ManagePage
    {//1e2124dd04e11d01b9df2865f85944be
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型
        private readonly PhotoService _service;
       
        wx_requestRule rBll = new wx_requestRule();

        public photoEdit()
        {
            _service = new  PhotoService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = MXRequest.GetQueryString("action");
            int id = 0;
            if (!string.IsNullOrEmpty(_action) && _action == MXEnums.ActionEnum.Edit.ToString())
            {

                this.action = MXEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out  id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!_service.Exists(id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {

                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(id);
                }
                else
                {
                    txtbeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtendDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }



        #region 赋值操作=================================
        //1e2124dd04e11d01b9df2865f85944be
        private void ShowInfo(int id)
        {
            hidid.Value = id.ToString();

            var dto = _service.GetModel(id);

            txtactName.Text = dto.actName;
            txtactContent.Value = dto.actContent;
            txtbrief.Value = dto.brief;
            txtbeginDate.Text = dto.beginDate;
            txtendDate.Text = dto.endDate;
        }

        #endregion



        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();

            Model.wx_requestRuleContent rc = new Model.wx_requestRuleContent();
            int id = MyCommFun.Str2Int(hidid.Value);
            #region  //先判断
            string strErr = "";
            if (this.txtKW.Text.Trim().Length == 0)
            {
                strErr += "关键词不能为空！";
            }
            if (this.txtactName.Text.Trim().Length == 0)
            {
                strErr += "活动名称不能为空！";
            }
            if (this.txtbeginDate.Text.Trim().Length == 0 || !MyCommFun.isDateTime(txtbeginDate.Text))
            {
                strErr += "开始时间不能为空！";
            }
            if (this.txtendDate.Text.Trim().Length == 0 || !MyCommFun.isDateTime(txtendDate.Text))
            {
                strErr += "结束时间不能为空！";
            }
            

            if (strErr != "")
            {
                JscriptMsg(strErr, "back", "Error");
                return;
            }
            DateTime begin = MyCommFun.Obj2DateTime(txtbeginDate.Text.Trim());
            DateTime end = MyCommFun.Obj2DateTime(txtendDate.Text.Trim());
            if (begin >= end)
            {
                JscriptMsg("开始时间必须小于结束时间", "back", "Error");
                return;
            }
            #endregion

            #region 赋值

            var dto = new photoActionDTO();
            Model.wx_requestRule rule = new Model.wx_requestRule();

          
          

            if (id > 0)
            {
                dto = _service.GetModel(id);
            }

            dto.actName = txtactName.Text.Trim();

            dto.brief = txtbrief.Value.Trim();
            dto.beginDate = begin.ToString();
            dto.endDate = end.ToString();
            dto.actContent = txtactContent.Value.Trim();
         
           
      

            #endregion

            if (id <= 0)
            {  //新增
                dto.wid = weixin.id;
                
                //1新增主表
                _service.Add(dto);
                id = dto.id;
                
                //3 新增回复规则表
                AddRule(weixin.id, id);
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加湖怪活动，主键为" + id); //记录日志//1e2124dd04e11d01b9df2865f85944be
                JscriptMsg("添加湖怪活动成功！", "photolist.aspx", "Success");
            }
            else
            {   //修改
                //1修改主表
                _service.Modify(dto);
                
                //3 修改回复规则表
                IList<Model.wx_requestRule> rlist = rBll.GetModelList("modelFunctionName = '湖怪' and modelFunctionId=" + id);

                if (rlist != null && rlist.Count > 0)
                {
                    rule = rlist[0];
                    rule.reqKeywords = txtKW.Text.Trim();
                    rBll.Update(rule);
                }
                else
                {
                    AddRule(weixin.id, id);
                }

                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改湖怪活动，主键为" + id); //记录日志
                JscriptMsg("修改湖怪活动成功！", "photolist.aspx", "Success");
            }

        }

        /// <summary>
        /// 添加回复规则
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="modelId"></param>
        private void AddRule(int wid, int modelId)
        {
            rBll.AddModeltxtPicRule(wid, "湖怪", modelId, txtKW.Text.Trim());
        }


       

        
    }
}