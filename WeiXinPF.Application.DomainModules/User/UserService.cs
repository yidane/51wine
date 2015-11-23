using System;
using AutoMapper;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess.User;

namespace WeiXinPF.Application.DomainModules.User
{
    public class UserService : IUserService
    {
        private readonly BLL.manager _managerDal;
        public UserService()
        {
            _managerDal = new manager();
            Mapper.CreateMap<WeiXinPF.Model.manager, UserDto>()
                .ForMember(dto => dto.UserId, u => u.MapFrom(m => m.id))
                .ForMember(dto => dto.LoginName, u => u.MapFrom(m => m.user_name))
                .ForMember(dto => dto.DisplayName, u => u.MapFrom(m => m.real_name));

            Mapper.CreateMap<UserDto, WeiXinPF.Model.manager>()
                .ForMember(dto => dto.id, u => u.MapFrom(m => m.UserId))
                .ForMember(dto => dto.user_name, u => u.MapFrom(m => m.LoginName))
                .ForMember(dto => dto.real_name, u => u.MapFrom(m => m.DisplayName));
        }

        public UserDto Get(int userId)
        {
            UserDto user = null;
            var account = _managerDal.GetModel(userId);

            if (account != null)
            {
                user = Mapper.Map<Model.manager, UserDto>(account);
            }
            return user;
        }

        public UserDto Get(string loginName, string password)
        {
            UserDto user = null;
            Model.manager account = _managerDal.GetModel(loginName, password);



            if (account != null)
            {
                user = Mapper.Map<Model.manager, UserDto>(account);
            }
            return user;
        }

        public UserDto Mapping(Model.manager user)
        {
            return user.MapTo<UserDto>();
        }

        public UserInfoEntity Get(string openid)
        {
            return UserInfoEntity.GetModel(openid);

        }
    }
}
