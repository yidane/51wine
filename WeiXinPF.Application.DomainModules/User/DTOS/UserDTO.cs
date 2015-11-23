

namespace WeiXinPF.Application.DomainModules.User.DTOS
{
    /// <summary>
    ///  用户
    /// </summary> 
    public class UserDto
    {
        public int UserId { get; set; }

        public string LoginName { get; set; }
        //       public string Mail {get;set;} 
        public string DisplayName { get; set; }
    }
}
