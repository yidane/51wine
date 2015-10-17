using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class shop_user_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString();
        protected int shopid = 0; //商铺ID
        private int id = 0;//管理员ID

        private int shop_admin_role = 17;
        protected void Page_Load(object sender, EventArgs e)
        {
            shopid = MXRequest.GetQueryInt("shopid");
            action = MXRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(action) && action == MXEnums.ActionEnum.Edit.ToString())
            {
                if (!int.TryParse(MXRequest.GetQueryString("id"), out id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }

                if (!new BLL.manager().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
            }

            if (!IsPostBack)
            {
                RoleBind();
                if (action == MXEnums.ActionEnum.Edit.ToString())
                {
                    ShowInfo(id);
                }
            }
        }

        #region 赋值操作
        private void ShowInfo(int id)
        {
            litpwdtip.Text = "不填则不修改密码";
            BLL.manager bll = new BLL.manager();

            Model.manager model = bll.GetModel(id);
            ddlRoleId.SelectedValue = model.role_id.ToString();

            if (model.is_lock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }

            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");

            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
            txtRemark.Text = model.remark;
        }
        #endregion
        #region 角色类型=================================
        private void RoleBind()
        {
            BLL.manager_role bll = new BLL.manager_role();
            DataTable dt = new DataTable();
            dt = bll.GetList("id in(18,19) ").Tables[0];

            ddlRoleId.Items.Clear();
            ddlRoleId.Items.Add(new ListItem("请选择角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                ddlRoleId.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion
        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.manager adminEntity = GetAdminInfo(); //取得管理员信息

            Model.manager model = new Model.manager();
            BLL.manager bll = new BLL.manager();

            //固定为餐饮管理员的角色

            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_role().GetModel(model.role_id).role_type;

            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }

            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }

            model.user_name = txtUserName.Text.Trim();
            //获得6位的salt加密字符串
            model.salt = Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.add_time = DateTime.Now;
            model.wxNum = 0;
            model.agentId = GetAdminInfo().id;
            model.qq = string.Empty;
            model.email = txtEmail.Text;
            model.reg_ip = MXRequest.GetIP();
            model.agentLevel = -1;
            model.remark = txtRemark.Text;
            model.agentId = adminEntity.id;
            int addId = bll.Add(model);

            if (addId > 0)
            {
                //添加商铺与管理人员的关联
                BLL.wx_diancai_shop_user shopUserBll = new BLL.wx_diancai_shop_user();
                Model.wx_diancai_shop_user shopUser = new Model.wx_diancai_shop_user();
                shopUser.ManagerId = addId;
                shopUser.ShopId = shopid;
                shopUser.AdminId = GetAdminInfo().id;
                int addShopAdminId = shopUserBll.Add(shopUser);

                if (addShopAdminId <= 0)
                {
                    bll.Delete(addId);
                    addId = 0;
                }

            }

            if (addId > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加餐饮商铺管理员:" + model.user_name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int id)
        {
            Model.manager adminEntity = GetAdminInfo(); //取得管理员信息

            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(id);

            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }

            //判断密码是否更改
            if (txtPassword.Text.Trim() != "")
            {
                //获取用户已生成的salt作为密钥加密
                model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            }

            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();

            model.remark = txtRemark.Text;

            bool updateRet = bll.Update(model);
            if (updateRet)
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改餐饮商铺管理员信息:" + model.user_name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 保存操作==================================
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改用户信息成功！", "shop_user_list.aspx?shopid=" + shopid, "Success");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加用户信息成功！", "shop_user_list.aspx?shopid=" + shopid, "Success");
            }
        }
        #endregion
    }
}