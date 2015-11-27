using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.Model.message;

namespace WeiXinPF.Application.DomainModules.User
{
    public interface IUserManagerService
    { 
        UserManagerDto Get(int userId);
        UserManagerDto Get(string loginName, string password);

        UserManagerDto Mapping(Model.manager user);

        MsgUserType GetUserType(UserManagerDto user);
    }
}
