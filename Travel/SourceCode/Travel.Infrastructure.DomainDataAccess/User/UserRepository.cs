using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.User.Entitys
{
    public class UserRepository
    {
        public void AddUser(UserInfo user)
        {
            using (var db = new TravelDBContext())
            {
                db.UserInfo.Add(user);
                db.SaveChanges();
            }
        }

        public bool IsHasUser(UserInfo user)
        {
            bool result = false;
            using (var db = new TravelDBContext())
            {
                var selectUser = db.UserInfo.FirstOrDefault(item => item.openid == user.openid);
                result = selectUser == null ? false : true;
            }
            return result;
        }

        public void UpdateUser(UserInfo user)
        {
            using (var db = new TravelDBContext())
            {
                db.UserInfo.Attach(user);
                db.SaveChanges();
            }
        }

        public void SaveUser(UserInfo user)
        {
            if (IsHasUser(user))
            {
                UpdateUser(user);
            }
            else
            {
                AddUser(user);
            }
        }

    }
}
