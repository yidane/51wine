using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class caipin_manage_add : Web.UI.ManagePage
    {
        BLL.wx_diancai_caipin_manage managebll = new BLL.wx_diancai_caipin_manage();
        Model.wx_diancai_caipin_manage manage = new Model.wx_diancai_caipin_manage();
        protected  int shopid = 0;
        public  string type = "";
        public  int ids = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = MyCommFun.QueryString("type");
            shopid = MyCommFun.RequestInt("shopid");
            ids = MyCommFun.RequestInt("id");
            if (!Page.IsPostBack)
            {
                //categoryName绑定
                BindCaiPingType();
                if (type == "edite")
                {
                    manage = managebll.GetModel(ids);
                    this.cpName.Text = manage.cpName;
                    this.number.Text = manage.number;
                    this.dllCategoryName.SelectedValue = manage.categoryid.ToString();
                    this.cpPrice.Text = manage.cpPrice.ToString();
                    this.zkPrice.Text = manage.zkPrice.ToString();
                    this.priceUnite.Text = manage.priceUnite;
                    this.cpPic.Text = manage.cpPic;
                    this.picUrl.Text = manage.picUrl;
                    this.detailContent.InnerText = manage.detailContent;
                    this.instructions.InnerText = manage.instructions;
                    this.shopIntroduction.InnerText = manage.shopIntroduction;
                    this.sortid.Text = manage.sortid.ToString();
                    this.chargeback.InnerText = manage.chargeback;
                    if (manage.beginDate != null)
                        this.txtbeginDate.Text=manage.beginDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    if (manage.endDate != null)
                        this.txtendDate.Text=manage.endDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        protected void BindCaiPingType()
        {
            BLL.wx_diancai_caipin_category cBll = new BLL.wx_diancai_caipin_category();
            IList<Model.wx_diancai_caipin_category> cateList = cBll.GetModelList("shopid=" + shopid);
            dllCategoryName.DataValueField = "id";
            dllCategoryName.DataTextField = "categoryName";
            dllCategoryName.DataSource = cateList;
            dllCategoryName.DataBind();
            dllCategoryName.Items.Insert(0, new ListItem("请选择", ""));

        }

        protected void save_caidanmanage_Click(object sender, EventArgs e)
        {



            if (type == "add")
            {
                manage.shopid = shopid;
                manage.categoryid = Convert.ToInt32(this.dllCategoryName.SelectedItem.Value);
                manage.categoryName = this.dllCategoryName.SelectedItem.Text;
                manage.cpName = this.cpName.Text;
                manage.cpPrice = Convert.ToDecimal(this.cpPrice.Text);
                manage.zkPrice = Convert.ToDecimal(this.zkPrice.Text);
                manage.priceUnite = this.priceUnite.Text;
                manage.cpPic = this.cpPic.Text;
                manage.picUrl = this.picUrl.Text;
                manage.detailContent = this.detailContent.InnerText;
                manage.createDate = DateTime.Now;

                manage.sortid = Convert.ToInt32(this.sortid.Text);

                //喀纳斯添加--
                manage.beginDate = DateTime.Parse(this.txtbeginDate.Text);
                manage.endDate = DateTime.Parse(this.txtendDate.Text);
                manage.instructions = this.instructions.InnerText;
                manage.shopIntroduction = this.shopIntroduction.InnerText;
                manage.chargeback = this.chargeback.InnerText;

                int id = managebll.Add(manage);
                if (id>0)
                {
                    manage = managebll.GetModel(id);
                    this.number.Text = manage.number;
                }

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加商品信息管理，主键为" + id); //记录日志
                JscriptMsg("增加成功！", Utils.CombUrlTxt("caipin_manage.aspx?shopid=" + shopid + "&manage=managetype", "keywords={0}", ""), "Success");

            }

            if (type == "edite")
            {
                shopid = MyCommFun.RequestInt("shopid");
                manage = managebll.GetModel(ids);
              
                manage.shopid = shopid;
                manage.categoryid = Convert.ToInt32(this.dllCategoryName.SelectedItem.Value);
                manage.categoryName = this.dllCategoryName.SelectedItem.Text;
                manage.cpName = this.cpName.Text;
                manage.cpPrice = Convert.ToDecimal(this.cpPrice.Text);
                manage.zkPrice = Convert.ToDecimal(this.zkPrice.Text);
                manage.priceUnite = this.priceUnite.Text;
                manage.cpPic = this.cpPic.Text;
                manage.picUrl = this.picUrl.Text;
                manage.detailContent = this.detailContent.InnerText;
                manage.sortid = Convert.ToInt32(this.sortid.Text);

                //喀纳斯添加--
                manage.beginDate = DateTime.Parse(this.txtbeginDate.Text);
                manage.endDate = DateTime.Parse(this.txtendDate.Text);
                manage.instructions = this.instructions.InnerText;
                manage.shopIntroduction = this.shopIntroduction.InnerText;
                manage.chargeback = this.chargeback.InnerText;

                managebll.Update(manage);

                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改商品信息管理，主键为" + ids); //记录日志
                JscriptMsg("修改成功！", Utils.CombUrlTxt("caipin_manage.aspx?shopid=" + shopid + "&manage=managetype", "keywords={0}", ""), "Success");


            }
        }
    }
}