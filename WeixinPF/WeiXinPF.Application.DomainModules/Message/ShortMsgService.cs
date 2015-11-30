using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Application.DomainModules.User;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess;
using WeiXinPF.Infrastructure.DomainDataAccess.Message;
using WeiXinPF.Model.message;

namespace WeiXinPF.Application.DomainModules.Message
{
    public class ShortMsgService : IShortMsgService
    {
        private readonly IUserManagerService _userService;
        private readonly IShortMsgRepository _msgRepository;

        public ShortMsgService()
        {
            var context = new WXDBContext();
            _msgRepository = new ShortMsgRepository(context);
            _userService = new UserManagerService();
            CreateMapping();
        }

        public void CreateMapping()
        {
            Mapper.CreateMap<ShortMsg, ShortMsgDto>()
                .ForMember(dto => dto.FromUser, msg => { msg.Ignore(); })
                .ForMember(dto => dto.ToUser, msg => { msg.Ignore(); })
                .ForMember(dto => dto.CreateTime, msg => msg.MapFrom(m => m.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.MsgToUserType,
                msg => msg.MapFrom(m => Enum.GetName(typeof(MsgUserType), m.MsgToUserType)))
                ;
            Mapper.CreateMap<ShortMsgDto, ShortMsg>()
                 .ForMember(msg => msg.CreateTime, dto => { dto.Ignore(); })
               ;

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


        public List<ShortMsgDto> GetMsgList(UserManagerDto toUserDto)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
            c.ToUserId == toUserDto.UserId).OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);

            return result;
        }

        public List<ShortMsgDto> GetMsgList(UserManagerDto toUserDto, UserManagerDto fromUserDto)
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

        private List<ShortMsgDto> MapData(IQueryable<ShortMsg> entityList, UserManagerDto user
            , bool isToUser = true)
        {
            List<ShortMsgDto> result = null;
            if (entityList.Any())
            {
                var list = entityList.MapTo<List<ShortMsgDto>>();
                //            var listSystems = _fromSystemService.GetList();
                list.ForEach(m =>
                {

                    m.FromUser = isToUser ? GetUser(m.FromUserId, m.MsgFromUserType) : user;
                    m.ToUser = isToUser ? user : GetUser(m.ToUserId, m.MsgToUserType);
                }
               );
                result = list;
            }


            return result;
        }

        public List<ShortMsgDto> GetMsgList(UserManagerDto toUserDto, int showCount)
        {
            List<ShortMsgDto> result = null;
            var entityList = _msgRepository.GetAllList(c =>
            c.ToUserId == toUserDto.UserId)
            .Take(showCount)
            .OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);
            return result;
        }

        public int GetMsgCount(UserManagerDto toUserDto)
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

        public List<ShortMsgDto> GetSendMsgList(UserManagerDto fromUserDto)
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

                result.FromUser = GetUser(result.FromUserId,result.MsgFromUserType);
                result.ToUser = GetUser(result.ToUserId, result.MsgToUserType);
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

        public ShortMsgDto GetLastNewMsg(UserManagerDto toUserDto, string fromUserId="")
        {
            ShortMsgDto result = null;

            if (toUserDto != null)
            {

                var userType = _userService.GetUserType(toUserDto);
                Expression<Func<ShortMsg, bool>> func;
                if (userType != MsgUserType.User)
                {
                    func = c =>
                        c.MsgToUserType == (int)userType && c.IsRead == false &&
                        (fromUserId == "" || c.FromUserId == fromUserId.ToString());

                }
                else
                {
                    func = c =>
                        c.ToUserId == toUserDto.UserId && c.IsRead == false &&
                        (fromUserId == "" || c.FromUserId == fromUserId.ToString());
                }


                var list = _msgRepository.GetAllList(func);
                var entity = list.OrderByDescending(c => c.CreateTime).FirstOrDefault();


                if (entity != null)
                {
                    result = entity.MapTo<ShortMsgDto>();

                    result.FromUser = GetUser(result.FromUserId,result.MsgFromUserType);
                    result.ToUser = toUserDto;
                }

            }

            return result;
        }


        /// <summary>
        /// 通过最新消息，来阅读所有未读消息
        /// </summary> 
        /// <param name="userId"></param>
        public void ReadAllNewMsg(string userId)
        {

            if (!string.IsNullOrEmpty(userId))
            {

                var shortMsgs = _msgRepository.GetAllList(c => c.IsRead == false
                && c.ToUserId == userId.ToString()).ToList();
                shortMsgs.ForEach(c =>
                {
                    c.IsRead = true;
                    _msgRepository.Modify(c);
                });
                _msgRepository.SaveChange();
            }
        }

        public List<ShortMsgDto> GetAllNewMsg(UserManagerDto toUserDto)
        {
            List<ShortMsgDto> result = null;

            var userType = _userService.GetUserType(toUserDto);
            Expression<Func<ShortMsg, bool>> func;
            if (userType != MsgUserType.User)
            {
                func = c =>
                    c.MsgToUserType == (int)userType && c.IsRead == false;

            }
            else
            {
                func = c =>
                    c.ToUserId == toUserDto.UserId && c.IsRead == false;
            }
            var entityList = _msgRepository.GetAllList(func).OrderByDescending(c => c.CreateTime);
            result = MapData(entityList, toUserDto);


            return result;
        }

        public void ReadAllNewMsg(string userId, string fromUserId)
        {
            if (!string.IsNullOrEmpty(userId ) && !string.IsNullOrEmpty(fromUserId))
            {

                var shortMsgs = _msgRepository.GetAllList(c => c.IsRead == false 
                && c.ToUserId == userId.ToString()
                && c.FromUserId == fromUserId.ToString()).ToList();
                shortMsgs.ForEach(c =>
                {
                    c.IsRead = true;
                    _msgRepository.Modify(c);
                });

                _msgRepository.SaveChange();
            }
        }

        public List<ShortMsgWithCountDto> GetAllLastNewMsg(UserManagerDto toUserDto)
        {
            List<ShortMsgWithCountDto> result = new List<ShortMsgWithCountDto>();
            if (toUserDto != null)
            {

                var userType = _userService.GetUserType(toUserDto);
                Expression<Func<ShortMsg, bool>> func;
                if (userType != MsgUserType.User)
                {
                    var userids = new List<string>();
                    var hotelBll = new BLL.wx_hotel_admin();
                    var diancaiBll=new BLL.wx_diancai_admin();
                    switch (userType)
                    {
                        case MsgUserType.Hotel:
                            var listU = hotelBll.GetModelList(String.Format(
                                "HotelId=(SELECT HotelId FROM dbo.wx_hotel_admin WHERE ManagerId={0})"
                                , toUserDto.UserId));
                            if (listU!=null&&listU.Any())
                            {
                                userids = listU.Select(i => i.ManagerId.ToString()).ToList();
                            }
                            break;
                            case MsgUserType.Shop:
                            var listD = diancaiBll.GetModelList(String.Format(
                                "ShopId=(SELECT ShopId FROM dbo.wx_diancai_admin WHERE ManagerId={0})"
                                , toUserDto.UserId));
                            if (listD != null && listD.Any())
                            {
                                userids = listD.Select(i => i.ManagerId.ToString()).ToList();
                            }
                            break;
                        case MsgUserType.Scenic:
                        default:
                            //景区管理员
                            var count = new wx_userweixin().GetUserWxNumCount(toUserDto.UserId.ToInt());
                            if (count > 0)
                            {
                                userids.Add(toUserDto.UserId);
                                 
                            }
                           
                            break;
                    }


                    func = c =>
                        c.MsgToUserType == (int)userType
                        && userids.Contains(c.ToUserId)  
                        && c.IsRead == false;

                }
                else
                {

                    func = c =>
                        c.ToUserId == toUserDto.UserId && c.IsRead == false;



                }
                var msgList = _msgRepository.GetAllList(func);
                //todo:判断组合方法
                //现在是如果不是微信用户发的，就按发送人分组
                //是微信用户发的直接分组
                var list = msgList.Where(c => (MsgUserType)c.MsgFromUserType
                != MsgUserType.WeChatCustomer).GroupBy(c => c.FromUserId)
               .Select(c =>
               new
               {
                   Count = c.Count(),
                   Key = c.Key
               }).ToList();

                if (list.Any())
                {
                    var listReslut = list.Select(c => new ShortMsgWithCountDto
                    {
                        Count = c.Count,
                        Msg = GetLastNewMsg(toUserDto, c.Key)
                    }).ToList();

                    //多个商品在商品描述上加上xx等5件商品
                    listReslut.ForEach(c =>
                    {
                        if (c.Count > 1)
                        {
                            var index = c.Msg.Content.LastIndexOf(']');

                            c.Msg.Content = c.Msg.Content.Insert(index + 1,
                                String.Format("等{0}件商品", c.Count));
                        }
                    });
                    result.AddRange(listReslut);
                }

                //添加是微信用户发的直接分组的
                if (msgList.Any(c=>(MsgUserType)c.MsgFromUserType== MsgUserType.WeChatCustomer))
                {
                    var wxlist = msgList.Where(c => (MsgUserType)c.MsgFromUserType
                == MsgUserType.WeChatCustomer).GroupBy(c => c.MsgFromUserType)
               .Select(c =>
               new
               {
                   Count = c.Count(),
                   Key = c.Max(u=>u.Id)
               }).ToList();

                    if (wxlist.Any())
                    {
                        var listReslut = wxlist.Select(c => new ShortMsgWithCountDto
                        {
                            Count = c.Count,
                            Msg = GetMsg(c.Key)
                        }).ToList();

                        //多个订单在商品描述上加上xx等5件订单
                        listReslut.ForEach(c =>
                        {
                            if (c.Count > 1)
                            {
                                var index = c.Msg.Content.LastIndexOf("的订单");
                                var msgContent = c.Msg.Content.Substring(0, index);
                                var msgHz = c.Msg.Content.Substring(c.Msg.Content.LastIndexOf("，"));
                                //todo:写死的判断多个时后缀
                                var hz = c.Msg.Type.ToLower().Contains("order") ? "订单" : "";
                                c.Msg.Content = String.Format("{2}等{0}个{1}{3}", c.Count, hz, msgContent, msgHz);
                            }
                        });
                        result.AddRange(listReslut);
                    }
                }
                
            }

            return result;
        }




        private UserManagerDto GetUser(string userId, MsgUserType msgUserType)
        {
            UserManagerDto result = null;

            if (msgUserType != MsgUserType.WeChatCustomer)
            {
                result = _userService.Get(userId.ToInt());
            }
            else
            {
                //todo:加上判断获取微信用户
            }



            return result;
        }


    }
}
