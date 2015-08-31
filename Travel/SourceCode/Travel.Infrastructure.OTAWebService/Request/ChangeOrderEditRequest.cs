using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Request
{
    public class ChangeOrderEditRequest
    {
        public EditPostOrder PostOrder { get; set; }
    }

    public class EditPostOrder
    {
        public string Ptime { get; set; }
        public List<EditOrderDetail> Details { get; set; }
        public EditOrder Order { get; set; }

        /// <summary>
        /// 修改代码，1：改期，2：申请退票
        /// </summary>
        public string Edittype { get; set; }

        public string Type
        {
            get { return "00"; }
        }

        public string parkCode
        {
            get { return OTAConfigManager.ParkCode; }
        }

        /// <summary>
        /// 无值默认全退，只支持门票
        /// </summary>
        public int Count { get; set; }

        public override string ToString()
        {
            if (Count > 0)
            {
                var request = new
                {
                    Ptime = Ptime,
                    Order = JSONHelper.Serialize(Order),
                    Details = JSONHelper.Serialize(Details),
                    Edittype = Edittype,
                    Type = Type,
                    Count = Count
                };

                return JSONHelper.Serialize(request);
            }
            else
            {
                var request = new
                {
                    Ptime = Ptime,
                    Order = JSONHelper.Serialize(Order),
                    Details = JSONHelper.Serialize(Details),
                    Edittype = Edittype,
                    Type = Type
                };

                return JSONHelper.Serialize(request);
            }
        }
    }

    public class EditOrder
    {
        public string OrderNo { get; set; }
    }

    public class EditOrderDetail
    {
        public string ProductCode { get; set; }
        public string Starttime { get; set; }
    }
}
