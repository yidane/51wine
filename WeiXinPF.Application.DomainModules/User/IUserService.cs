using WeiXinPF.Application.DomainModules.User.DTOS;

namespace WeiXinPF.Application.DomainModules.User
{
    public interface IUserService
    { 
        UserDto Get(int userId);
        UserDto Get(string loginName, string password);

        UserDto Mapping(Model.manager user);
    }
}
