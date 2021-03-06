﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using WeiXinPF.Application.DomainModules.Message;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Application.DomainModules.User;
using WeiXinPF.Application.DomainModules.User.DTOS;
using WeiXinPF.Common;

namespace WeiXinPF.WebService
{
    public class MessageWebService : BaseWebService
    {
        private readonly IShortMsgService _msgService;
        private readonly IUserManagerService _userService;
        public MessageWebService()
        {
            _msgService = new ShortMsgService();
            _userService = new UserManagerService();
        }



        /// <summary>
        /// 发送消息
        /// </summary> 
        /// <returns></returns> 
        [Obsolete("方法已经停用，以后再用")]
        public void SendMsg(string strMsg)
        {
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var msg = JSONHelper.Deserialize<ShortMsgDto>(strMsg);
                msg.FromUserId = user.id.ToString();
                _msgService.SendMsg(msg);
                Context.Response.Write(AjaxResult.Success(null).ToCamelString() );
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public void GetMsgList()
        {

            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var userDto = _userService.Get(user.id);
                var list = _msgService.GetMsgList(userDto);
                Context.Response.Write(AjaxResult.Success(list));
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code));
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 获取定量的消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Obsolete("方法已经停用，以后再用")]

        public void GetMsgListWithCount(int count)
        {

            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var userDto = _userService.Get(user.id);
                var list = _msgService.GetMsgList(userDto, count);
                Context.Response.Write(AjaxResult.Success(list));
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }


        /// <summary>
        /// 获取接收的消息数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Obsolete("方法已经停用，以后再用")]

        public void GetNewMsgCount()
        { 
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var userDto = _userService.Get(user.id);
                var count = _msgService.GetMsgCount(userDto);
                Context.Response.Write(AjaxResult.Success(count).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }

        /// <summary>
        /// 获取最新的消息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
             [Obsolete("方法已经停用，以后再用")]

        public void GetNewMsg()
        {
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var result = new ShortMsgWithCountDto() { Count = 0 };
                var userDto = _userService.Get(user.id);
                var count = _msgService.GetMsgCount(userDto);
                var shortMsgDto = _msgService.GetLastNewMsg(userDto);

                if (shortMsgDto != null)
                {
                    result.Msg = shortMsgDto;

                }
                result.Count = count;
                Context.Response.Write(AjaxResult.Success(result));
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code));
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 获取最新的消息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Obsolete("方法已经停用，以后再用")]

        public void GetNewMsg(int id)
        {
            
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var result = new ShortMsgWithCountDto() { Count = 0 };
                var userDto = _userService.Get(user.id);
                var count = _msgService.GetMsgCount(userDto);
                var shortMsgDto = _msgService.GetMsg(id);

                if (shortMsgDto != null)
                {
                    result.Msg = shortMsgDto;

                }
                result.Count = count;
                Context.Response.Write(AjaxResult.Success(result).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }

        /// <summary>
        /// 阅读新消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [Obsolete("方法已经停用，以后再用")]

        public void ReadNewMsg(int msgId)
        {
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                _msgService.ReadNewMsg(msgId);
                Context.Response.Write(AjaxResult.Success(null).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }

        /// <summary>
        /// 阅读用户所有新消息
        /// </summary> 
        /// <param name="fromUserId"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public void ReadAllNewMsg(string fromUserId)
        {
            
            try
            {

                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                _msgService.ReadAllNewMsg(user.id.ToString(), fromUserId);
                Context.Response.Write(AjaxResult.Success(null).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }


        /// <summary>
        /// 获取所有接收的新消息（用户维度）
        /// </summary> 
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public void GetAllLastNewMsg()
        {
 

            try
            {

                var reult = new ListMsgWithCount() { Count = 0 };
                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }
                var userDto = _userService.Get(user);
                var list = _msgService.GetAllLastNewMsg(userDto);
                //            list = list.Take(3).ToList();
                if (list != null && list.Any())
                {
                    reult.Count = list.Sum(c => c.Count);
                    reult.Msgs = list; 
                }
                Context.Response.Write(AjaxResult.Success(reult).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }

        /// <summary>
        /// 获取所有聊天记录
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="fromUserId"></param>
        /// <returns></returns> 
        [Obsolete("方法已经停用，以后再用")]

        public void GetAllLastNewMsg(string fromUserId)
        {
            try
            {
                var reult = new ListMsgWithCount() { Count = 0 };
                var user = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (user == null)
                {
                    throw new Exception("用户未登陆！");
                }

                var userDto = _userService.Get(user.id);
                var fromUserDto = new UserManagerDto() {UserId = fromUserId};
                var list = _msgService.GetMsgList(userDto, fromUserDto);
                Context.Response.Write(AjaxResult.Success(list).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }



        /// <summary>
        /// 获取所有类型消息列表(短消息、邮件、待办等)
        /// </summary> 
        /// <returns></returns>
        [Obsolete("方法已经停用，以后再用")]
        public void GetAllMsgList()
        {

            try
            {
                var reult = new ListMsgWithCount() { Count = 0 };
                var currentLoginUser = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (currentLoginUser == null)
                {
                    throw new Exception("用户未登陆！");
                }

                List<MessageDto> result = new List<MessageDto>();
                int index = 1;
                int draw = MyCommFun.QueryString("draw").ToInt();
                int start = MyCommFun.QueryString("start").ToInt();
                int length = MyCommFun.QueryString("length").ToInt();
                string search = MyCommFun.QueryString("search.value");
                var returnData = new DatatablesResult<List<MessageDto>>();
                //获取短消息
                var userDto = _userService.Get(currentLoginUser);
                var list = _msgService.GetAllLastNewMsg(userDto);
                if (list != null && list.Any())
                {
                    var newList = list.Select(c => new MessageDto()
                    {
                        Number = index++,
                        Id = c.Msg.Id,
                        Title = String.Format("{0}条新消息", c.Count),
                        Content = c.Msg.Content,
                        CreateTime = c.Msg.CreateTime,
                        FromUserId = c.Msg.FromUserId,
                     
                        FromUserName = c.Msg.FromUser.DisplayName
                    }).ToList();
                    result.AddRange(newList);
                };



                returnData.RecordsTotal = result.Count();
                returnData.RecordsFiltered = returnData.RecordsTotal;

                if (string.IsNullOrEmpty(search))
                {
                    returnData.Data = result.Skip(start).Take(length).ToList();
                }
                else
                {
                    returnData.Data = result.Where(c =>

                       
                        c.FromUserName.Contains(search)
                //                    ||c.Content.Contains(search)
                ).Skip(start).Take(length).ToList();
                }


                returnData.Draw = draw;
                Context.Response.Write(AjaxResult.Success(returnData).ToCamelString());
            }
            catch (UnAuthException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.RedirectUrl, jsEx.Code).ToCamelString());
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message).ToCamelString());
            }
        }
    }
}
