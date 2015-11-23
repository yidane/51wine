using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Application.DomainModules.User.DTOS;

namespace WeiXinPF.Application.DomainModules.Message
{
    public interface IShortMsgService
    {
        /// <summary>
        /// 发送短消息
        /// </summary>
        void SendMsg(ShortMsgDto msg);
        /// <summary>
        /// 接收的所有信息列表
        /// </summary>
        /// <param name="toUserDto"></param>
        /// <returns></returns>
        List<ShortMsgDto> GetMsgList(UserDto toUserDto);

        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <param name="toUserDto"></param>
        /// <param name="fromUserDto"></param>
        /// <returns></returns>
        List<ShortMsgDto> GetMsgList(UserDto toUserDto, UserDto fromUserDto);

        /// <summary>
        /// 接收的所有信息列表
        /// </summary>
        /// <param name="toUserDto"></param>
        /// <param name="showCount">显示的数量</param>
        /// <returns></returns>
        List<ShortMsgDto> GetMsgList(UserDto toUserDto, int showCount);
        /// <summary>
        /// 获取消息数量
        /// </summary>
        /// <param name="toUserDto"></param>
        /// <returns></returns>
        int GetMsgCount(UserDto toUserDto);
        /// <summary>
        /// 发送的信息列表
        /// </summary>
        /// <param name="fromUserDto"></param>
        /// <returns></returns>
        List<ShortMsgDto> GetSendMsgList(UserDto fromUserDto);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="msg"></param>
        void DeleteMsg(ShortMsgDto msg);
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        ShortMsgDto GetMsg(int msgId);

        /// <summary>
        /// 获取最新消息
        /// </summary> 
        /// <returns></returns>
        ShortMsgDto GetLastNewMsg(UserDto toUserDto, int toUserId = 0);

        /// <summary>
        /// 阅读所有新信息
        /// </summary>
        /// <param name="msgId"></param>
        void ReadNewMsg(int msgId);

        /// <summary>
        /// 阅读用户所有未读
        /// </summary> 
        void ReadAllNewMsg(int userId);


        /// <summary>
        /// 获取所有最新消息（人纬度）
        /// </summary> 
        /// <returns></returns>
        List<ShortMsgWithCountDto> GetAllLastNewMsg(UserDto toUserDto);

        /// <summary>
        /// 阅读所有发送人发的消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fromUserId"></param>
        void ReadAllNewMsg(int userId, int fromUserId);
    }
}
