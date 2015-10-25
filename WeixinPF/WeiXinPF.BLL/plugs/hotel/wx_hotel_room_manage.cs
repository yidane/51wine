using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

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

        public void ManageRoom(int roomid, Model.RoomStatus status, int operatorid, string operateName, string comment = "")
        {
            DAL.wx_hotel_room roomDal = new DAL.wx_hotel_room();
            Model.wx_hotel_room model = roomDal.GetModel(roomid);
            model.Status = status;

            using (TransactionScope scope = new TransactionScope())
            {
                roomDal.Update(model);

                Model.wx_hotel_room_manage manageInfo = new Model.wx_hotel_room_manage();
                manageInfo.RoomId = model.id;
                manageInfo.Operator = operatorid;
                manageInfo.OperateName = operateName;
                manageInfo.OperateTime = DateTime.Now;
                manageInfo.Comment = comment;
                _dal.Add(manageInfo);

                scope.Complete();
            }
        }

        public string GetComment(int roomid)
        {
           return  _dal.GetComment(roomid);
        }
    }
}
