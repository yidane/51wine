using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CityServerPF.Messages.RequestResponse;
using NServiceBus;

namespace CityServerPF.Client.Web
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                return;
            }
            #region ActionHandling
            var categoryRequest = new RestaurantCategoryRequest()
            {
                ShopId = 1,
                Wid = 1
            };

            IAsyncResult res = Global.Bus.Send("CityServerPF.Server.Restautant", categoryRequest).Register(response =>
                                {
                                    CompletionResult localResult = (CompletionResult)response.AsyncState;
                                    var category = localResult.Messages[0] as RestaurantCategoryResponse;

                                    TextBox1.Text = category.CategoryName;
                                }, this);
            WaitHandle asyncWaitHandle = res.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            //Global.Bus.Send("CityServerPF.Server.Restautant", categoryRequest).Register(response =>
            //{
            //    var category = (RestaurantCategoryResponse)response.Messages[0];

            //    TextBox1.Text = category.CategoryName;
            //});
            #endregion
        }
    }
}