using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Wine.WebServices
{
    public class WebServiceResult<T> where T : class
    {
        public bool IsSucceed { get; set; }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }

        public WebServiceResult(bool isSucceed, T t, string errorMessage)
        {
            this.IsSucceed = isSucceed;
            this.Result = t;
            this.ErrorMessage = errorMessage;
        }

        public WebServiceResult(bool isSucceed, string errorMessage)
        {
            this.IsSucceed = isSucceed;
            this.ErrorMessage = errorMessage;
        }

        public WebServiceResult(bool isSucceed, T t)
        {
            this.IsSucceed = isSucceed;
            this.Result = t;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
