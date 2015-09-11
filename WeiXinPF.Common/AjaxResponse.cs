using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WeiXinPF.Common
{
    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse()
        {
        }

        public AjaxResponse(bool success)
            : base(success)
        {
        }

        public AjaxResponse(object result)
            : base(result)
        {

        }

        public AjaxResponse(ErrorInfo error)
            : base(error)
        {

        }
    }
    public class AjaxResponse<TResult>
    {
        public bool Success { get; set; }

        public TResult Result { get; set; }

        public ErrorInfo Error { get; set; }

        public AjaxResponse()
        {
            Success = true;
        }

        public AjaxResponse(bool success)
        {
            Success = success;
        }

        public AjaxResponse(ErrorInfo error)
        {
            Error = error;
            Success = false;
        }

        public AjaxResponse(TResult result)
        {
            Success = true;
            Result = result;
        }

        public override string ToString()
        {
            return JSONHelper.Serialize(this, true);
        }
    }
}
