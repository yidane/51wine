using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Presentation.WebPlugin.Product;

namespace Travel.Presentation.WebPlugin
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        private int m_ProductID = 0;
        private int m_ProductPackageId = 0;
        private string m_ProductSource = string.Empty;
        private bool m_IsAdd = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitQueryString();
            if (!IsPostBack)
            {
                if (!m_IsAdd)
                {
                    var productEntity = new ProductEntityManager().GetProductEntity(m_ProductID, m_ProductPackageId, m_ProductSource);
                    BindData(productEntity);
                }
                else
                {
                    this.txtProductCategoryId.Text = Guid.NewGuid().ToString();
                }
            }
        }

        private void InitQueryString()
        {
            m_ProductSource = Request.QueryString["ProductSource"];
            //表示是编辑模式
            if (!string.IsNullOrEmpty(m_ProductSource))
            {
                m_IsAdd = false;
                var produceId = Request.QueryString["ProductID"];
                var productPackageID = Request.QueryString["ProductPackageID"];

                if (!(int.TryParse(produceId, out m_ProductID) && int.TryParse(productPackageID, out m_ProductPackageId)))
                    throw new Exception("参数异常");
            }
        }

        private void BindData(ProductEntity entity)
        {
            if (entity == null)
                return;

            this.txtCurrentStatus.Text = entity.CurrentStatus;
            this.txtFirstSort.Text = entity.FirstSort;
            this.txtOldProductName.Text = entity.OldProductName;
            this.txtProductCategoryId.Text = entity.ProductCategoryId.ToString();
            this.txtProductCategoryId.Enabled = false;
            this.txtProductCode.Text = entity.ProductCode;
            this.txtProductDescription.Text = entity.ProductDescription;
            this.txtProductID.Text = entity.ProductId.ToString();
            this.txtProductID.Enabled = false;
            this.txtProductName.Text = entity.ProductName;
            this.txtProductPackageID.Text = entity.ProductPackageId.ToString();
            this.txtProductPrice.Text = entity.ProductPrice.ToString();
            this.txtProductSource.Text = entity.ProductSource;
            this.txtProductSource.Enabled = false;
            this.txtProductType.Text = entity.ProductType;
            this.txtSecondSort.Text = entity.SecondSort;

            this.cboIsCombinedProduct.Checked = entity.IsCombinedProduct;
            this.cboIsSelfDefinedProduct.Checked = entity.IsSelfDefinedProduct;
        }

        public ProductEntity CollectData()
        {
            var productEntity = new ProductEntity();
            productEntity.CurrentStatus = this.txtCurrentStatus.Text;
            productEntity.FirstSort = this.txtFirstSort.Text;
            productEntity.OldProductName = this.txtOldProductName.Text;
            productEntity.ProductCategoryId = Guid.Parse(this.txtProductCategoryId.Text);
            productEntity.ProductCode = this.txtProductCode.Text;
            productEntity.ProductDescription = this.txtProductDescription.Text;
            productEntity.ProductId = int.Parse(this.txtProductID.Text);
            productEntity.ProductName = this.txtProductName.Text;
            productEntity.ProductPackageId = int.Parse(this.txtProductPackageID.Text);
            productEntity.ProductPrice = decimal.Parse(this.txtProductPrice.Text);
            productEntity.ProductSource = this.txtProductSource.Text;
            productEntity.ProductType = this.txtProductType.Text;
            productEntity.SecondSort = this.txtSecondSort.Text;
            productEntity.IsCombinedProduct = this.cboIsCombinedProduct.Checked;
            productEntity.IsSelfDefinedProduct = this.cboIsSelfDefinedProduct.Checked;

            return productEntity;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var productEntity = CollectData();
                new ProductEntityManager().InsertOrUpdate(productEntity, m_IsAdd);
                AlertAndRedirect("保存数据成功", "ProductList.aspx");
            }
            catch (Exception exception)
            {
                Alert(exception.Message);
            }
        }

        private void Alert(string message)
        {
            var script = "<script type='text/javascript'>alert('{0}')</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format(script, message));
        }

        private void AlertAndRedirect(string message, string url)
        {
            var script = "<script type='text/javascript'>alert('{0}');document.location.href='{1}';</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format(script, message, url));
        }
    }
}