using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Entity
{
    public class Customer
    {
        public int UserID { get; set; }
        public string WeiXinID { get; set; }
        public string DisplayName { get; set; }
        public DateTime Birthday { get; set; }
        public string Mobile { get; set; }
    }
}