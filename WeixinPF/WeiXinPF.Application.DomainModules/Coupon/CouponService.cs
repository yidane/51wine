using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WeiXinPF.Application.DomainModules.Coupon.DTOS;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess.User;
using WeiXinPF.Model;

namespace WeiXinPF.Application.DomainModules.Coupon
{
    public class CouponService
    {
        //private CouponRepository _repository;
        private UserInfoEntity _user;
        private readonly BLL.wx_dzpActionInfo _actBll;
        private readonly BLL.wx_dzpUsersTemp _utbll;
        private readonly BLL.wx_dzpAwardUser _ubll;
        private readonly BLL.wx_dzpAwardItem _itemBll;
        //当活动未过期时,返回的空优惠券
        //private readonly CouponDTO _noCoupon = new CouponDTO() {
        //    title = "已参与",
        //    subTitle = "谢谢参与",
        //    status = CouponState.BeParticipatedIn
        //};
        public CouponService()
        {

            _actBll = new BLL.wx_dzpActionInfo();
            _utbll = new BLL.wx_dzpUsersTemp();
            _ubll = new BLL.wx_dzpAwardUser();
            _itemBll = new BLL.wx_dzpAwardItem();
            //_repository = new CouponRepository();
            //_userRepository = new UserRepository();
        }

        /// <summary>
        /// 获取摇一摇基本信息，并保存用户
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="wxUser"></param>
        /// <returns></returns>
        public CouponBaseInfoDTO GetCouponBaseInfo(int aid, OAuthUserInfo wxUser)
        {
            CouponBaseInfoDTO result = null;
            var dzpAction = _actBll.GetModel(aid);
            if (dzpAction != null)
            {
                int hasCjTimes = 0;
                if (!string.IsNullOrEmpty(wxUser.openid))
                {
                    hasCjTimes = _utbll.getCJCiShu(aid, wxUser.openid);//返回该用户的抽奖次数
                }

                #region 保存用户信息

                if (wxUser != null)
                {
                    //保存微信用户信息
                    var user = new UserInfoEntity()
                    {
                        openid = wxUser.openid,
                        // groupid = wxUser.groupid,
                        headimgurl = wxUser.headimgurl,
                        // language = wxUser.language,
                        province = wxUser.province,
                        // remark = wxUser.remark,
                        // subscribe = wxUser.subscribe,
                        //  subscribe_time = wxUser.subscribe_time,
                        nickname = wxUser.nickname,
                        sex = wxUser.sex,
                        city = wxUser.country,
                        country = wxUser.country
                    };
                    user.SaveUser();
                }

                #endregion

                result = new CouponBaseInfoDTO()
                {
                    actName = dzpAction.actName,
                    brief = dzpAction.brief,
                    contractInfo = dzpAction.contractInfo,
                    personMaxTimes = dzpAction.personMaxTimes,
                    dayMaxTimes = dzpAction.dayMaxTimes,
                    hasCjTimes = hasCjTimes,
                    djPwd = dzpAction.djPwd,
                    cfcjhf=dzpAction.cfcjhf,
                    beginDate = dzpAction.beginDate?.ToString() ?? "",
                    endDate = dzpAction.endDate?.ToString() ?? "",
                    beginPic= MyCommFun.getWebSite() + dzpAction.beginPic,
                    endNotice = dzpAction.endNotice,
                    endContent = dzpAction.endContent,
                    endPic = MyCommFun.getWebSite() + dzpAction.endPic
                };
            }


            return result;
        }


        //public CouponDTO GetCoupon(string openId, Guid couponUsageId)
        //{
        //    CouponDTO result = null;

        //    if (!string.IsNullOrEmpty(openId))
        //    {
        //        var coupon = _repository.GetCouponByUser(couponUsageId, openId);
        //        if (coupon != null)
        //        {
        //            result = TransformCoupon(coupon);
        //        }
        //    }

        //    return result;
        //}



        //public CouponListDTO GetCouponList(UserInfoDTO wxUser)
        //{
        //    CouponListDTO result = null;

        //    if (wxUser != null)
        //    {
        //        var user = new UserInfo()
        //        {
        //            openid = wxUser.openid,
        //            groupid = wxUser.groupid,
        //            headimgurl = wxUser.headimgurl,
        //            language = wxUser.language,
        //            province = wxUser.province,
        //            remark = wxUser.remark,
        //            subscribe = wxUser.subscribe,
        //            subscribe_time = wxUser.subscribe_time,
        //            nickname = wxUser.nickname,
        //            sex = wxUser.sex,
        //            city = wxUser.country,
        //            country = wxUser.country
        //        };
        //        result = new CouponListDTO();
        //        var expiredList = _repository.GetExpiredCoupons(user);
        //        if (expiredList != null)
        //        {
        //            result.ExpiredCoupons = expiredList.Select(p => new CouponDTO()
        //            {
        //                title = p.Coupon.Title,
        //                subTitle = p.Coupon.SubTitle,
        //                beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
        //                couponId = p.CouponId,

        //                couponType = p.Coupon.Type.CouponTypeName,
        //                endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
        //                couponUsageId = p.CouponUsageId
        //            }).ToList();
        //        }

        //        var unExpiredList = _repository.GetUnExpiredCoupons(user);
        //        if (unExpiredList != null)
        //        {
        //            result.UnExpiredCoupons = unExpiredList.Select(p => new CouponDTO()
        //            {
        //                title = p.Coupon.Title,
        //                subTitle = p.Coupon.SubTitle,
        //                beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
        //                couponId = p.CouponId,
        //                couponType = p.Coupon.Type.CouponTypeName,
        //                endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
        //                couponUsageId = p.CouponUsageId
        //            }).ToList();
        //        }
        //        var usedList = _repository.GetUsedCoupons(user);
        //        if (usedList != null)
        //        {
        //            result.UsedCoupons = usedList.Select(p => new CouponDTO()
        //            {
        //                title = p.Coupon.Title,
        //                subTitle = p.Coupon.SubTitle,
        //                beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
        //                couponId = p.CouponId,
        //                couponType = p.Coupon.Type.CouponTypeName,
        //                endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
        //                usedTime = p.UsedTime == null ? string.Empty
        //                  : p.UsedTime.Value.ToString("yyyy-MM-dd"),
        //                couponUsageId = p.CouponUsageId
        //            }).ToList();
        //        }


        //    }

        //    return result;
        //}



        ///// <summary>
        ///// 获取随机优惠券
        ///// </summary>
        ///// <param name="openId"></param>
        ///// <returns></returns>
        //public CouponDTO GetRandomCoupon(UserInfoDTO wxUser)
        //{
        //    CouponDTO result = null;
        //    //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
        //    //   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        //    //{
        //        if (!BeParticipatedInCoupon(wxUser))
        //    {







        //            var availableCoupons = _repository.GetAvailableCoupon();

        //        if (availableCoupons.Any())
        //        {

        //            Infrastructure.DomainDataAccess.Coupon.Entitys.Coupon coupon =
        //                _repository.GetRandomCoupon(availableCoupons);

        //            if (coupon != null)
        //            {
        //                //保存微信用户信息
        //                var user = new UserInfo()
        //                {
        //                    openid = wxUser.openid,
        //                    groupid = wxUser.groupid,
        //                    headimgurl = wxUser.headimgurl,
        //                    language = wxUser.language,
        //                    province = wxUser.province,
        //                    remark = wxUser.remark,
        //                    subscribe = wxUser.subscribe,
        //                    subscribe_time = wxUser.subscribe_time,
        //                    nickname = wxUser.nickname,
        //                    sex = wxUser.sex,
        //                    city = wxUser.country,
        //                    country = wxUser.country
        //                };
        //                _userRepository.SaveUser(user);

        //                //摇到就立即领取,如空优惠券
        //                if (coupon.ShakeToReceive)
        //                {
        //                    AddCouponToUser(user.openid, coupon.CouponId);
        //                    result = TransformCoupon(coupon, CouponState.HasReceived);
        //                }
        //                else
        //                {
        //                    result = TransformCoupon(coupon);

        //                }



        //            }
        //        }
        //        else
        //        {
        //            result = _noCoupon;
        //        }



        //    }
        //    else
        //    {
        //        result = _noCoupon;
        //    }
        //    //    scope.Complete();
        //    //}
        //    return result;
        //}

        //public void AddCouponToUser(string openid, Guid couponId)
        //{
        //    if (couponId != null && !string.IsNullOrEmpty(openid))
        //    {

        //        var coupon = _repository.GetCoupon(couponId);

        //        _repository.AddCouponToUser(openid, coupon);
        //    }
        //}

        ///// <summary>
        ///// 判断是否参与过本次摇奖
        ///// </summary>
        ///// <returns></returns>
        //public bool BeParticipatedInCoupon(UserInfoDTO wxUser)
        //{
        //    bool result = false;

        //    result = _repository.BeParticipatedInCoupon(wxUser.openid);

        //    return result;
        //}



        ///// <summary>
        ///// 判断是否还是有优惠券
        ///// </summary>
        ///// <returns></returns>
        //public bool HasCoupon()
        //{
        //    bool result = false;

        //    result = _repository.HasCoupon();

        //    return result;
        //}



        ///// <summary>
        ///// 获取指定优惠券,默认未领取
        ///// </summary>
        ///// <param name="openId"></param>
        ///// <param name="coupon"></param>
        ///// <returns></returns>
        //private CouponDTO TransformCoupon(Infrastructure.DomainDataAccess.Coupon.Entitys.Coupon coupon

        //        , CouponState status = CouponState.NotReceived)
        //{
        //    CouponDTO result = null;

        //    if (coupon != null)
        //    {
        //        result = new CouponDTO()
        //        {
        //            couponId = coupon.CouponId,
        //            //openId = user.OpenId,
        //            title = coupon.Title,
        //            subTitle = coupon.SubTitle,
        //            beginTime = coupon.BeginTime.ToString("yyyy-MM-dd"),
        //            endTime = coupon.EndTime.ToString("yyyy-MM-dd"),
        //            getTime = DateTime.Now.ToString("yyyy-MM-dd"),
        //            couponType = coupon.Type.CouponTypeName,
        //            status = status

        //        };
        //    }

        //    return result;
        //}

        //private CouponDTO TransformCoupon(CouponUsage coupon)
        //{
        //    CouponDTO result = null;

        //    if (coupon != null)
        //    {
        //        result = new CouponDTO()
        //        {
        //            couponId = coupon.CouponId,
        //            //openId = user.OpenId,
        //            title = coupon.Coupon.Title,
        //            subTitle = coupon.Coupon.SubTitle,
        //            beginTime = coupon.Coupon.BeginTime.ToString("yyyy-MM-dd"),
        //            endTime = coupon.Coupon.EndTime.ToString("yyyy-MM-dd"),
        //            getTime = DateTime.Now.ToString("yyyy-MM-dd"),
        //            couponType = coupon.Coupon.Type.CouponTypeName,
        //            status = coupon.State,
        //            usedTime = coupon.UsedTime == null ? string.Empty :
        //            coupon.UsedTime.Value.ToString("yyyy-MM-dd")
        //        };
        //    }

        //    return result;
        //}

        //public void UseCoupon(string openId, Guid id)
        //{
        //    if (!string.IsNullOrEmpty(openId))
        //    {
        //        _repository.UseCoupon(openId, id);
        //    }
        //}





        /// <summary>
        /// 获取随机优惠券
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="aid"></param>
        /// <param name="wid"></param>
        /// <returns></returns>
        public CouponPrizeDTO GetRandomCoupon(string openid, int aid, int wid)
        {
            CouponPrizeDTO result = null;

            //抽奖
            #region 判断活动

            if (aid == 0 || wid == 0 || openid.Trim() == "")
            {
                throw new JsonException("参数错误！", "system");

            }
            var dzpAction = _actBll.GetModel(aid);
            if (dzpAction == null)
            {
                throw new JsonException("参数错误！", "system");
            }

            if (dzpAction.endDate <= DateTime.Now)
            {
                throw new JsonException("活动已结束", "end");

            }
            else if (dzpAction.beginDate > DateTime.Now)
            {
                throw new JsonException("活动未开始", "nostart");


            }






            int dayMaxTimes = dzpAction.dayMaxTimes ?? 0;
            //int perMaxTimes = dzpAction.personMaxTimes ?? 0;
            //判断每人最大抽奖次数，是否超过了
            if (personCJTimes(openid, aid) >= dzpAction.personMaxTimes)
            {
                throw new JsonException("您已抽过奖了，欢迎下次再来！", "notimes");

            }
            if (isTodayOverSum(aid, openid, dayMaxTimes))
            {
                throw new JsonException("每人每天只有" + dayMaxTimes.ToString() + "次抽奖机会。", "notimes");


            }
            Model.wx_dzpAwardUser award = _ubll.getZJinfoByOpenid(aid, openid);
            if (award != null)
            {
                throw new JsonException("您中奖了，欢迎下次再来！", "notimes");

            }


            #endregion

            //#region 保存用户信息

            //if (wxUser != null)
            //{
            //    //保存微信用户信息
            //    var user = new UserInfoEntity()
            //    {
            //        openid = wxUser.openid,
            //        // groupid = wxUser.groupid,
            //        headimgurl = wxUser.headimgurl,
            //        // language = wxUser.language,
            //        province = wxUser.province,
            //        // remark = wxUser.remark,
            //        // subscribe = wxUser.subscribe,
            //        //  subscribe_time = wxUser.subscribe_time,
            //        nickname = wxUser.nickname,
            //        sex = wxUser.sex,
            //        city = wxUser.country,
            //        country = wxUser.country
            //    };
            //    user.SaveUser();
            //}

            //#endregion

            #region 计算中奖信息

            /// 处理是否中奖
            /// hidStatus 状态为-1：不能抽奖，直接跳转到end.aspx页面；
            /// 0：抽奖次数超过设置的最高次数；
            /// 1：还可以继续抽奖；
            /// 2：中奖了；

            List<Model.wx_dzpAwardItem> itemlist = _itemBll.GetModelList("actId=" + aid);//该活动的所有奖项信息
            int ttJpNum = 0;
            for (int i = 0; i < itemlist.Count; i++)
            {
                ttJpNum += itemlist[i].jpRealNum.Value;
            }


            IList<Model.wx_dzpAwardUser> auserlist = _ubll.getHasZJList(aid);//已经中奖的人列表
            int ZhongJiangNum = 0;
            if (auserlist != null)
            {
                ZhongJiangNum = auserlist.Count; //已经中奖的人数
            }

            int syZjNum = ttJpNum - ZhongJiangNum; //剩余的奖品数量
            if (syZjNum <= 0)
            {
                //说明已经没有奖品了
                throw new JsonException(dzpAction.cfcjhf, "nomore");

            }
            dzpAction.personNum = MyCommFun.Obj2Int(dzpAction.personNum, 1);
            dzpAction.personMaxTimes = MyCommFun.Obj2Int(dzpAction.personMaxTimes, 1);
            int fenmo = dzpAction.personNum.Value * dzpAction.personMaxTimes.Value;

            Random rd = new Random((int)DateTime.Now.Ticks);
            int radNum = rd.Next(0, fenmo);//从0到fenmo里随机出一个值
            if (radNum < syZjNum)
            {
                //中奖了，再从剩余奖品里抽取一个奖品
                Model.wx_dzpAwardItem dajiang = getZJItem(itemlist, auserlist);
                if (dajiang != null)
                {
                    //这是中的中奖了
                    string snumber = Get_snumber(aid);
                    int uId = _ubll.Add(aid, "", "", openid, dajiang.jxName, dajiang.jpName, snumber);
                    result = new CouponPrizeDTO()
                    {
                        sortid = dajiang.sort_id.Value,
                        jxname = dajiang.jxName,
                        jpname = dajiang.jpName,
                        uid = uId,
                        sn = snumber,
                        getTime = DateTime.Now.ToString("yy-MM-dd HH-mm-ss")
                    };


                }
                else
                {
                    //说明已经没有奖品了
                    throw new JsonException(dzpAction.cfcjhf, "nomore");
                }

            }
            else
            {
                //这个条件说明：未中奖
                //抛出未中奖的数据 
                //说明已经没有奖品了
                throw new JsonException(dzpAction.cfcjhf, "nomore");

            }




            #endregion
            return result;
        }



        #region 私有方法

        /// <summary>
        /// 取中奖的项目
        /// </summary>
        /// <param name="itemlist">所有的奖品信息</param>
        /// <param name="haszjlist">已经中奖的列表</param>
        /// <returns></returns>
        private Model.wx_dzpAwardItem getZJItem(IList<Model.wx_dzpAwardItem> itemlist, IList<Model.wx_dzpAwardUser> haszjlist)
        {
            IList<Model.wx_dzpAwardItem> zjItemlist = new List<Model.wx_dzpAwardItem>();//剩余奖品列表

            Model.wx_dzpAwardItem tmpItem = new Model.wx_dzpAwardItem();
            Model.wx_dzpAwardItem stmpItem = new Model.wx_dzpAwardItem();
            IList<Model.wx_dzpAwardUser> thiszjRs;

            for (int i = 0; i < itemlist.Count; i++)
            {
                tmpItem = itemlist[i];
                thiszjRs = (from user in haszjlist where user.jpName == tmpItem.jpName && user.jxName == tmpItem.jxName select user).ToArray<Model.wx_dzpAwardUser>();
                int tmpSYNum = 0;
                if (thiszjRs != null)
                {
                    tmpSYNum = MyCommFun.Obj2Int(tmpItem.jpRealNum) - thiszjRs.Count;
                }
                if (tmpSYNum <= 0)
                {
                    continue;
                }
                for (int j = 0; j < tmpSYNum; j++)
                {
                    stmpItem = new Model.wx_dzpAwardItem();
                    stmpItem.jpName = tmpItem.jpName;
                    stmpItem.jxName = tmpItem.jxName;
                    stmpItem.sort_id = tmpItem.sort_id;
                    zjItemlist.Add(stmpItem);
                }
            }

            Random rd = new Random((int)DateTime.Now.Ticks);
            int jpIndex = rd.Next(0, zjItemlist.Count);//从0到zjItemlist.Count里随机出一个值
            return zjItemlist[jpIndex];
        }


        /// <summary>
        /// 判断该用户的抽奖次数
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        private int personCJTimes(string openid, int aid)
        {
            int times = 0;
            times = _utbll.GetRecordCount("actId=" + aid + " and openid='" + openid + "'");
            return times;
        }

        /// <summary>
        /// 判断今天是否已经超出抽奖次数
        /// todayTTTimes:能抽奖的总次数
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="todayTTTimes">每天的抽奖总次数</param>
        /// <returns></returns>
        private bool isTodayOverSum(int aid, string openid, int todayTTTimes)
        {
            if (todayTTTimes <= 0)
            {
                return true;
            }

            Model.wx_dzpUsersTemp model = new Model.wx_dzpUsersTemp();
            model.openid = openid;
            DateTime todaybegin = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime mingtianBegin = todaybegin.AddDays(1);
            if (!_utbll.ExistsOpenid(" actId=" + aid + "  and  openid='" + openid + "' and  createDate>='" + todaybegin + "' and createDate<'" + mingtianBegin + "'"))
            { //第一次，插入
                model.times = 1;
                model.createDate = DateTime.Now;
                model.openid = openid;
                model.actId = aid;
                _utbll.Add(model);
                return false;
            }

            model = _utbll.getModelByAidOpenid(aid, openid);
            if (model.times >= todayTTTimes)
            {
                return true;
            }
            else
            {
                model.times += 1;
                _utbll.Update(model);
                return false;
            }

        }

        /// <summary>
        /// 返回中奖序列号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get_snumber(int aid)
        {
            Random rd = new Random((int)DateTime.Now.Ticks);
            int radNum = rd.Next(0, 9);//从0到9里随机出一个值

            return "SNyyy" + aid + "_" + MyCommFun.ConvertDateTimeInt(DateTime.Now) + radNum;
        }

        #endregion
    }
}
