using System;
using AutoMapper;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess.User;
using WeiXinPF.Model.message;
using wx_diancai_admin = WeiXinPF.Model.wx_diancai_admin;

namespace WeiXinPF.Application.DomainModules.User
{
    public class UserManagerService : IUserManagerService
    {
        private readonly BLL.manager _managerDal;
        public UserManagerService()
        {
            _managerDal = new manager();
            Mapper.CreateMap<WeiXinPF.Model.manager, UserManagerDto>()
                .ForMember(dto => dto.UserId, u => u.MapFrom(m => m.id.ToString()))
                .ForMember(dto => dto.LoginName, u => u.MapFrom(m => m.user_name))
                .ForMember(dto => dto.DisplayName, u => u.MapFrom(m => m.real_name))
                .ForMember(dto => dto.RoleId, u => u.MapFrom(m => m.role_id))
                .ForMember(dto => dto.RoleType, u => u.MapFrom(m => m.role_type))
                ;

            Mapper.CreateMap<UserManagerDto, WeiXinPF.Model.manager>()
                .ForMember(dto => dto.id, u => u.MapFrom(m => int.Parse(m.UserId)))
                .ForMember(dto => dto.user_name, u => u.MapFrom(m => m.LoginName))
                .ForMember(dto => dto.role_id, u => u.MapFrom(m => m.RoleId))
                .ForMember(dto => dto.role_type, u => u.MapFrom(m => m.RoleType))
                .ForMember(dto => dto.real_name, u => u.MapFrom(m => m.DisplayName));
        }

        public UserManagerDto Get(int userId)
        {
            UserManagerDto user = null;
            var account = _managerDal.GetModel(userId);

            if (account != null)
            {
                user = Mapper.Map<Model.manager, UserManagerDto>(account);
            }
            return user;
        }

        public UserManagerDto Get(string loginName, string password)
        {
            UserManagerDto user = null;
            Model.manager account = _managerDal.GetModel(loginName, password);



            if (account != null)
            {
                user = Mapper.Map<Model.manager, UserManagerDto>(account);
            }
            return user;
        }

        public UserManagerDto Get(Model.manager user)
        {
            return user.MapTo<UserManagerDto>();
        }

        

        public MsgUserType GetUserType(UserManagerDto user)
        {
            MsgUserType result = MsgUserType.User;

            //酒店管理员
            var wxHotelAdmin = new BLL.wx_hotel_admin();
            int intUser = 0;
            if (int.TryParse(user.UserId,out intUser))
            {
                var hotelAdmin = wxHotelAdmin.GetModel(intUser);
                if (hotelAdmin != null)
                {

                    result = MsgUserType.Hotel;
                    return result;
                }

                //餐饮管理员
                var wxDiancaiAdmin = new BLL.wx_diancai_admin();
                var diancaiAdmin = wxDiancaiAdmin.GetModel(intUser);
                if (diancaiAdmin != null)
                {

                    result = MsgUserType.Shop;
                    return result;
                }

                //todo:是否需要判断固定的微信号？
                //景区管理员
                var count = new wx_userweixin().GetUserWxNumCount(intUser);
                if (count > 0)
                {
                    result = MsgUserType.Scenic;
                    return result;
                }
            }
            else
            {
                //todo:判断微信是否是用户
                //如果userid不是Int型 ,默认就认为它是微信用户
                result= MsgUserType.WeChatCustomer;
                return result;
            }
            

            return result;
        }
    }
}
