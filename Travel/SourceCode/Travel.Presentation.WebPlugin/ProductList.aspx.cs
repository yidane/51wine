using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Presentation.WebPlugin.Product;

namespace Travel.Presentation.WebPlugin
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPruductList();
            }
        }

        private void BindPruductList()
        {
            var productList = new ProductEntityManager().GetProductEntityList();
            this.rptList1.DataSource = productList;
            this.rptList1.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            //BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            rptList = this.rptList1;

            var hasDelete = false;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                var productId = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hdProductID")).Value);
                var productPackageID = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hdProductPackageID")).Value);
                var productSource = ((HiddenField)rptList.Items[i].FindControl("hdProductSource")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    new ProductEntityManager().DeleteEntity(productId, productPackageID, productSource);
                    hasDelete = true;
                }
            }

            if (hasDelete)
            {
                AlertAndRedirect("删除成功", "ProductList.aspx");  
            }
            else
            {
                AlertAndRedirect("请选择一条记录后再删除", "ProductList.aspx");
            }
        }

        private void AlertAndRedirect(string message, string url)
        {
            var script = "<script type='text/javascript'>alert('{0}');document.location.href='{1}';</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format(script, message, url));
        }
    }
}