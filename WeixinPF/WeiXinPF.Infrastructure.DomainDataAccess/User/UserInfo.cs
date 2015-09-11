using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.User
{
    [Table("tb_UserInfo")]
    public partial class UserInfoEntity
    {

        #region Properties
        [Key]
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public Int64 subscribe_time { get; set; }
        public string remark { get; set; }
        public int groupid { get; set; }
        public string subscribe { get; set; }
        #endregion


    
    }

    public partial class UserInfoEntity
    {
        #region Method
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        public void AddUser()
        {
            using (var db = new WXDBContext())
            {
                db.UserInfo.Add(this);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsHasUser()
        {
            bool result = false;
            using (var db = new WXDBContext())
            {
                var selectUser = db.UserInfo.FirstOrDefault(item => item.openid == this.openid);
                result = selectUser != null;
            }
            return result;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser()
        {
            using (var db = new WXDBContext())
            {
                db.UserInfo.Attach(this);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user"></param>
        public void SaveUser()
        {
            if (IsHasUser())
            {
                UpdateUser();
            }
            else
            {
                AddUser();
            }
        }
        #endregion
    }

}
