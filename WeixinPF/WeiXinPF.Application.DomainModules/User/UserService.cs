using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess.Photo;
using WeiXinPF.Infrastructure.DomainDataAccess.User;

namespace WeiXinPF.Application.DomainModules.User
{
   public  class UserService
    {
       public UserService()
        {
            Mapper.CreateMap<UserInfoEntity, UserDTO>();
            Mapper.CreateMap<UserDTO, UserInfoEntity>();
        }

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="userDto"></param>
       public void SaveUser(UserDTO userDto)
       {
           if (userDto!=null)
           {
               var user = Mapper.Map<UserDTO, UserInfoEntity>(userDto);
               user.SaveUser();
               
           }
       }

       /// <summary>
       /// 获取用户
       /// </summary>
       /// <param name="openid"></param>
       /// <returns></returns>
       public UserDTO GetModel(string openid)
       {
           UserDTO result = null;
           var info = UserInfoEntity.GetModel(openid);
           if (info != null)
           {
               result = Mapper.Map<UserInfoEntity, UserDTO>(info);
           }
           return result;
       }

    }
}
