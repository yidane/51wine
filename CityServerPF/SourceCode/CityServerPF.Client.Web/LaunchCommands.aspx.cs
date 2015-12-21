using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CityServerPF.Messages.Commands;
using CityServerPF.Messages.RequestResponse;
using NServiceBus;


public partial class LaunchCommands : Page
    {

        protected void btnCheckRestMoney_Click(object sender, EventArgs e)
        {
            var obj = new CheckRestMoney() {IdentifyId = "420111198709285521", UserId = Guid.NewGuid().ToString()};

            bool isDebug = true;
            Global.Bus.SetMessageHeader(obj, "Debug", isDebug.ToString());

            Global.Bus.Send(obj);
        }

        protected void btnRestaurantCategory_Click(object sender, EventArgs e)
        {
            var categoryRequest=new RestaurantCategoryRequest()
            {
                ShopId = 1,
                Wid = 1
            };

            Global.Bus.Send("CityServerPF.Server.Restautant", categoryRequest).Register(response =>
            {
                var category = (RestaurantCategoryResponse)response.Messages[0];

                txtCategoryName.Text = category.CategoryName;
            });
        }
}