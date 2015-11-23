using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Application.DomainModules.User;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess;
using WeiXinPF.Infrastructure.DomainDataAccess.Message;

namespace WeiXinPF.Application.DomainModules.Message
{
    public class ShortMsgService : IShortMsgService
    {
        private readonly IUserService _userService;
        private readonly IShortMsgRepository _msgRepository;

        public ShortMsgService()
        {
            var context = new WXDBContext();
            _msgRepository = new ShortMsgRepository(context);
            _userService = new UserService();
            CreateMapping();
        }

        public void CreateMapping()
        {
            Mapper.CreateMap<ShortMsg, ShortMsgDto>()
                .ForMember(dto => dto.FromUser, msg => { msg.Ignore(); })
                .ForMember(dto => dto.ToUser, msg => { msg.Ignore(); });
            Mapper.CreateMap<ShortMsgDto, ShortMsg>();

        }

        /// <summary>
        /// 发送短消息
        /// </summary>
        public void SendMsg(ShortMsgDto msg)
        {

            var shortMsg = msg.MapTo<ShortMsg>();
            if (msg.Id <= 0)
            {
                AddMsg(shortMsg);
            }
            else
            {
                ModifyMsg(shortMsg);
            }
            _msgRepository.SaveChange();

        }

        private void AddMsg(ShortMsg msgEntity)
        {
            msgEntity.CreateTime = DateTime.Now;
            _msgRepository.Add(msgEntity);
        }

        private void ModifyMsg(ShortMsg msgEntity)
        {
            _msgRepository.Modify(msgEntity);
        }


        public List<ShortMsgDto> GetMsgList(UserDto toUserDto)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
            c.ToUserId == toUserDto.UserId).OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);

            return result;
        }

        public List<ShortMsgDto> GetMsgList(UserDto toUserDto, UserDto fromUserDto)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
           ((c.ToUserId == toUserDto.UserId && c.FromUserId == fromUserDto.UserId) ||
           (c.FromUserId == toUserDto.UserId && c.ToUserId == fromUserDto.UserId)
           ))
            .OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);

            return result;
        }

        private List<ShortMsgDto> MapData(IQueryable<ShortMsg> entityList, UserDto user
            , bool isToUser = true)
        {
            List<ShortMsgDto> result = null;
            if (entityList.Any())
            {
                var list = entityList.MapTo<List<ShortMsgDto>>();
                //            var listSystems = _fromSystemService.GetList();
                list.ForEach(m =>
                {

                    m.FromUser = isToUser ? GetUser(m.FromUserId) : user;
                    m.ToUser = isToUser ? user : GetUser(m.ToUserId);
                }
               );
                result = list;
            }


            return result;
        }

        public List<ShortMsgDto> GetMsgList(UserDto toUserDto, int showCount)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
            c.ToUserId == toUserDto.UserId)
            .Take(showCount)
            .OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);
            return result;
        }

        public int GetMsgCount(UserDto toUserDto)
        {
            int result = 0;

            if (toUserDto != null)
            {
                var list = _msgRepository.GetAllList(c =>
                    c.ToUserId == toUserDto.UserId && c.IsRead == false);
                result = list.Count();
            }

            return result;
        }

        public List<ShortMsgDto> GetSendMsgList(UserDto fromUserDto)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
            c.FromUserId == fromUserDto.UserId);
            result = MapData(entityList, fromUserDto, false);


            return result;
        }

        public void DeleteMsg(ShortMsgDto msg)
        {
            var shortMsg = msg.MapTo<ShortMsg>();
            _msgRepository.Delete(shortMsg);
            _msgRepository.SaveChange();
        }

        public ShortMsgDto GetMsg(int msgId)
        {
            ShortMsgDto result = null;
            var entity = _msgRepository.Get(msgId);
            if (entity != null)
            {
                result = entity.MapTo<ShortMsgDto>();

                result.FromUser = GetUser(result.FromUserId);
                result.ToUser = GetUser(result.ToUserId);
            }


            return result;
        }


        public void ReadNewMsg(int msgId)
        {
            var shortMsg = _msgRepository.Get(msgId);
            if (shortMsg != null)
            {
                shortMsg.IsRead = true;
                _msgRepository.Modify(shortMsg);
                _msgRepository.SaveChange();
            }
        }

        public ShortMsgDto GetLastNewMsg(UserDto toUserDto, int toUserId = 0)
        {
            ShortMsgDto result = null;

            if (toUserDto != null)
            {
                var list = _msgRepository.GetAllList(c =>
                    c.ToUserId == toUserDto.UserId && c.IsRead == false && (toUserId == 0 || c.FromUserId == toUserId));
                var entity = list.OrderByDescending(c => c.CreateTime).FirstOrDefault();


                if (entity != null)
                {
                    result = entity.MapTo<ShortMsgDto>();

                    result.FromUser = GetUser(result.FromUserId);
                    result.ToUser = toUserDto;
                }

            }

            return result;
        }


        /// <summary>
        /// 通过最新消息，来阅读所有未读消息
        /// </summary> 
        /// <param name="userId"></param>
        public void ReadAllNewMsg(int userId)
        {

            if (userId > 0)
            {

                var shortMsgs = _msgRepository.GetAllList(c => c.IsRead == false
                && c.ToUserId == userId).ToList();
                shortMsgs.ForEach(c =>
                {
                    c.IsRead = true;
                    _msgRepository.Modify(c);
                });
                _msgRepository.SaveChange();
            }
        }

        public void ReadAllNewMsg(int userId, int fromUserId)
        {
            if (userId > 0 && fromUserId > 0)
            {

                var shortMsgs = _msgRepository.GetAllList(c => c.IsRead == false && c.ToUserId == userId
                && c.FromUserId == fromUserId).ToList();
                shortMsgs.ForEach(c =>
                {
                    c.IsRead = true;
                    _msgRepository.Modify(c);
                });

                _msgRepository.SaveChange();
            }
        }

        public List<ShortMsgWithCountDto> GetAllLastNewMsg(UserDto toUserDto)
        {
            List<ShortMsgWithCountDto> result = null;

            if (toUserDto != null)
            {
                var list = _msgRepository.GetAllList(c =>
                    c.ToUserId == toUserDto.UserId && c.IsRead == false).GroupBy(c => c.FromUserId)
                    .Select(c =>
                    new
                    {
                        Count = c.Count(),
                        Key = c.Key
                    }).ToList();

                if (list.Any())
                {
                    result = list.Select(c => new ShortMsgWithCountDto
                    {
                        Count = c.Count,
                        Msg = GetLastNewMsg(toUserDto, c.Key)
                    }).ToList();
                }



            }

            return result;
        }




        private UserDto GetUser(int userId)
        {
            UserDto result = null;
            result = _userService.Get(userId);

            return result;
        }


    }
}
