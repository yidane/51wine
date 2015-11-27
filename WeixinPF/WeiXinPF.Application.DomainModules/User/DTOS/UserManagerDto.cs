

namespace WeiXinPF.Application.DomainModules.User.DTOS
{
    /// <summary>
    ///  用户
    /// </summary> 
    public class UserManagerDto
    {
        public string UserId { get; set; }

        public string LoginName { get; set; }
        //       public string Mail {get;set;} 
        public string DisplayName { get; set; }
        public int RoleId { get; set; }
        public int RoleType { get; set; }
    }
    

}
