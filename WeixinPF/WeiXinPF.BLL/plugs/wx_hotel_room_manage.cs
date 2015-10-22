using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_hotel_room_manage
    {
        private DAL.wx_hotel_room_manage _dal = null;

        public wx_hotel_room_manage()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_hotel_room_manage();
            }
        }

        public int Add(Model.wx_hotel_room_manage model)
        {
            return _dal.Add(model);
        }
    }
}
