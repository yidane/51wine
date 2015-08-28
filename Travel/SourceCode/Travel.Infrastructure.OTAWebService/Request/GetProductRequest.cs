using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Request
{
    public class GetProductRequest
    {
        public ProductParameter parameters { get; set; }

        public GetProductRequest(DateTime dateTime)
        {
            this.parameters = new ProductParameter(dateTime);
        }
    }

    public class ProductParameter
    {
        public string date { get; set; }
        public string type
        {
            get { return "00"; }
        }
        public string parkCode
        {
            get { return OTAConfigManager.ParkCode; }
        }

        public ProductParameter(DateTime dateTime)
        {
            this.date = dateTime.ToString("yyyy-MM-dd");
        }

        public override string ToString()
        {
            return JSONHelper.Serialize(this);
        }
    }
}