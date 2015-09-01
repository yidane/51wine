using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Travel.Infrastructure.DomainDataAccess.User
{
    public class UserContracts
    {
        [Key]
        public string OpenID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string IdCard { get; set; }

        public void SaveNew()
        {
            using (var db = new TravelDBContext())
            {
                //移除掉以前的数据
                if (db.UserContract.Any())
                {
                    var theOnlyOne = db.UserContract.FirstOrDefault();
                    db.UserContract.Remove(theOnlyOne);
                }
                db.UserContract.Add(this);
                db.SaveChanges();
            }
        }

        public static UserContracts Get(string openId)
        {
            using (var db = new TravelDBContext())
            {
                return db.UserContract.Any() ? db.UserContract.FirstOrDefault(item => string.Equals(item.OpenID, openId)) : null;
            }
        }
    }
}
