/**************************************
 *
 * 作者：李~~朴
 * 公司:上·海沐~雪网·络·科·技有·限·公·司
 * qq:2~3~0~0~2~8~0~7
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2015-8-3
 * update:2015-8-3
 * 
 ***********************************/

using OneGulp.WeChat.MP.AdvancedAPIs;
using OneGulp.WeChat.MP.AdvancedAPIs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.WeiXinComm
{
    public  class WeiXinUser
    {
        #region Base Function: access_token和所有关注用户的openid字符串

        /// <summary>
        /// 获得所有关注用户的openid字符串（别的方法调用此方法）
        /// </summary>
        /// <returns></returns>
        private IList<string> baseUserOpenid(int wid, out string error)
        {
            IList<string> ret = new List<string>();

            string access_token = WeiXinCRMComm.getAccessToken(wid, out error);
            if (error != "")
            {
                return null;
            }
            OpenIdResultJson openidJson = UserApi.Get(access_token, "");
            if (openidJson.count == openidJson.total)
            {
                ret = openidJson.data.openid;
            }
            else
            {
                getNextUserOpenid(wid, openidJson.next_openid, ret);
            }

            return ret;
        }
        /// <summary>
        /// （基础方法）获得所有关注用户的openid字符串
        /// 递归算法
        /// </summary>
        /// <param name="nexOpenid"></param>
        /// <param name="openidList"></param>
        private void getNextUserOpenid(int wid, string nexOpenid, IList<string> openidList)
        {
            string err = "";
            string access_token = WeiXinCRMComm.getAccessToken(wid, out err);
            OpenIdResultJson openidJson = UserApi.Get(access_token, nexOpenid);

            if (openidJson == null || openidJson.count <= 0)
            {
                //return openidJson.data.openid;
                return;
            }
            else
            {
                for (int i = 0; i < openidJson.data.openid.Count; i++)
                {
                    openidList.Add(openidJson.data.openid[i]);
                }
                getNextUserOpenid(wid, openidJson.next_openid, openidList);
            }


        }

        /// <summary>
        /// 【从本地数据库里】获得关注用户的openid字符串
        ///与微信服务器上的数据有最大20分钟的出入
        /// </summary>
        /// <returns></returns>
        //public IList<string> getUserOpenidFromDataStroe()
        //{
        //    IList<string> ret = new List<string>();

        //    MxWeiXin.Model.wx_interface_info interfaceEntity = new MxWeiXin.Model.wx_interface_info();
        //    interfaceEntity = pBll.GetModelList("iName='useropenid'")[0];
        //    TimeSpan chajun = DateTime.Now - interfaceEntity.createDate.Value;
        //    double chajunSecond = chajun.TotalSeconds;
        //    if (interfaceEntity.iContent == null || interfaceEntity.iContent.Trim() == "" || chajunSecond >= interfaceEntity.expires_in)
        //    {

        //        //更新到本地数据库里
        //        IList<string> openidList = baseUserOpenid();
        //        string tmpOpenidStr = "";
        //        foreach (string tmpStr in openidList)
        //        {
        //            tmpOpenidStr += tmpStr.ToString() + ";";
        //        }
        //        interfaceEntity.iContent = tmpOpenidStr;
        //        interfaceEntity.createDate = DateTime.Now;
        //        pBll.Update(interfaceEntity);


        //    }
        //    else
        //    {
        //        string[] openidArr = interfaceEntity.iContent.Split(';');
        //        for (int i = 0; i < openidArr.Length; i++)
        //        {
        //            if (openidArr[i] != "")
        //            {
        //                ret.Add(openidArr[i]);
        //            }
        //        }
        //    }
        //    return ret;
        //}


        /// <summary>
        /// 【强制刷新】【从本地数据库里】获得关注用户的openid字符串
        ///与微信服务器上的数据有最大20分钟的出入
        /// </summary>
        /// <returns></returns>
        //public IList<string> FlushUserOpenidFromDataStroe()
        //{
        //    IList<string> ret = new List<string>();

        //    MxWeiXin.Model.wx_interface_info interfaceEntity = new MxWeiXin.Model.wx_interface_info();
        //    interfaceEntity = pBll.GetModelList("iName='useropenid'")[0];
        //    TimeSpan chajun = DateTime.Now - interfaceEntity.createDate.Value;
        //    double chajunSecond = chajun.TotalSeconds;
        //    //更新到本地数据库里
        //    IList<string> openidList = baseUserOpenid();
        //    string tmpOpenidStr = "";
        //    foreach (string tmpStr in openidList)
        //    {
        //        tmpOpenidStr += tmpStr.ToString() + ";";
        //    }
        //    interfaceEntity.iContent = tmpOpenidStr;
        //    interfaceEntity.createDate = DateTime.Now;
        //    pBll.Update(interfaceEntity);
        //    return ret;
        //}


        #endregion


        /// <summary>
        /// 获得所有关注用户信息
        /// </summary>
        /// <returns></returns>
        //public IList<UserInfoJson> getAllUserInfoList()
        //{
        //    IList<string> openidStr = getUserOpenidFromDataStroe();
        //    List<UserInfoJson> userlist = new List<UserInfoJson>();
        //    string access_token = getAccessToken();
        //    foreach (string openidValue in openidStr)
        //    {
        //        userlist.Add(OneGulp.WeChat.MP.AdvancedAPIs.User.Info(access_token, openidValue));
        //    }

        //    return userlist;
        //}


        //public IList<UserInfoJson> getUserInfoListPage(string whereStr, int PageSize, int CurrentPageIndex, out int RecordCount, string orderStr)
        //{
        //    IList<UserInfoJson> alluserlist = getAllUserInfoList();
        //    RecordCount = alluserlist.Count();


        //    return alluserlist;
        //}
    }
}
