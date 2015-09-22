using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Map.DTOS
{
    public class MapDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
    }

    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
