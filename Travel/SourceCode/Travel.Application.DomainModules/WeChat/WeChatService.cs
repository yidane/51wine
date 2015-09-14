using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.WeiXin.Common;
using Travel.Infrastructure.WeiXin.Common.OAuth2;
using Travel.Infrastructure.WeiXin.Common.Ticket;
using Travel.Infrastructure.WeiXin.Statistics;
using Travel.Infrastructure.WeiXin.User;

namespace Travel.Application.DomainModules.WeChat
{
    public class WeChatService
    {
        public SignatureDTO CreateSignatureDto(string url)
        {
            var rtnSignatureDTO = new SignatureDTO();
            var weChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
            var jsapiTicket = new ConfigTicket(new Credential(weChatAccountManager));

            rtnSignatureDTO.appId = weChatAccountManager.AppId;
            rtnSignatureDTO.nonceStr = jsapiTicket.CreateRandomString();
            rtnSignatureDTO.timestamp = jsapiTicket.CreateTimeStamp();
            var ticket = jsapiTicket.CreateTicket();
            rtnSignatureDTO.signature = jsapiTicket.CreateSignature(ticket, rtnSignatureDTO.nonceStr, rtnSignatureDTO.timestamp, url);

            return rtnSignatureDTO;
        }

        public CardTicketDTO CreateCardTicket()
        {
            var rtnCardTicketDTO = new CardTicketDTO();
            var weChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
            var cardTicket = new CardTicket(new Credential(weChatAccountManager));
            rtnCardTicketDTO.timestamp = cardTicket.CreateTimeStamp();
            rtnCardTicketDTO.nonceStr = cardTicket.CreateRandomString();
            rtnCardTicketDTO.signType = "SHA1";
            rtnCardTicketDTO.cardSign = cardTicket.CreateTicket();

            return rtnCardTicketDTO;
        }

        public AccessTokenDTO GetAccessToken(string code)
        {
            var accessToken = new AccessToken(WeChatAccountManager.CreateDefaultInstance());
            var accessTokenResult = accessToken.GetBaseAsseccTokenResult(code);
            return new AccessTokenDTO(accessTokenResult);
        }

        public UserInfoDTO GetUserInfo(string openId)
        {
            var result = new UserInfoHelper().GetUserInfoByOpenID(openId);
            return result == null ? null : new UserInfoDTO()
                {
                    subscribe = result.subscribe,
                    openid = result.openid,
                    nickname = result.nickname,
                    sex = result.sex,
                    language = result.language,
                    city = result.city,
                    province = result.province,
                    country = result.country,
                    headimgurl = result.headimgurl,
                    subscribe_time = result.subscribe_time,
                    remark = result.remark,
                    groupid = result.groupid
                };
        }

        public List<UserStatisticsDTO> GetUserStatistics(DateTime beginDate, DateTime endDate)
        {
            var rtnUserStatisticsList = new List<UserStatisticsDTO>();
            var result = new UserStatistics().GetUserStatistics(beginDate, endDate);
            if (result != null && result.list != null && result.list.Count > 0)
            {
                foreach (UserStatisticsInfo info in result.list)
                {
                    rtnUserStatisticsList.Add(new UserStatisticsDTO()
                        {
                            date = info.ref_date,
                            user_source = info.user_source,
                            user_sourceDesc = info.user_sourceDesc,
                            new_user = info.new_user,
                            cancel_user = info.cancel_user
                        });
                }
            }

            return rtnUserStatisticsList;
        }
    }
}
