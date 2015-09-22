using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Travel.Application.DomainModules.Coupon.DTOS;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.DomainDataAccess.Coupon;
using Travel.Infrastructure.DomainDataAccess.Coupon.Entitys;
using Travel.Infrastructure.DomainDataAccess.User;
using Travel.Infrastructure.DomainDataAccess.User.Entitys;
using Travel.Infrastructure.WeiXin.Extra;

namespace Travel.Application.DomainModules.Coupon
{
    public class CouponService
    {
        private CouponRepository _repository;
        private UserRepository _userRepository;
        //当活动未过期时,返回的空优惠券
        private readonly CouponDTO _noCoupon = new CouponDTO() {
            title = "已参与",
            subTitle = "谢谢参与",
            status = CouponState.BeParticipatedIn
        };
        public CouponService()
        {
            _repository = new CouponRepository();
            _userRepository = new UserRepository();
        }

        public CouponDTO GetCoupon(string openId, Guid couponUsageId)
        {
            CouponDTO result = null;

            if (!string.IsNullOrEmpty(openId))
            {
                var coupon = _repository.GetCouponByUser(couponUsageId, openId);
                if (coupon != null)
                {
                    result = TransformCoupon(coupon);
                }
            }

            return result;
        }



        public CouponListDTO GetCouponList(UserInfoDTO wxUser)
        {
            CouponListDTO result = null;

            if (wxUser != null)
            {
                var user = new UserInfo()
                {
                    openid = wxUser.openid,
                    groupid = wxUser.groupid,
                    headimgurl = wxUser.headimgurl,
                    language = wxUser.language,
                    province = wxUser.province,
                    remark = wxUser.remark,
                    subscribe = wxUser.subscribe.ToString(),
                    subscribe_time = wxUser.subscribe_time,
                    nickname = wxUser.nickname,
                    sex = wxUser.sex,
                    city = wxUser.country,
                    country = wxUser.country
                };
                result = new CouponListDTO();
                var expiredList = _repository.GetExpiredCoupons(user);
                if (expiredList != null)
                {
                    result.ExpiredCoupons = expiredList.Select(p => new CouponDTO()
                    {
                        title = p.Coupon.Title,
                        subTitle = p.Coupon.SubTitle,
                        beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
                        couponId = p.CouponId,

                        couponType = p.Coupon.Type.CouponTypeName,
                        endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
                        couponUsageId = p.CouponUsageId
                    }).ToList();
                }

                var unExpiredList = _repository.GetUnExpiredCoupons(user);
                if (unExpiredList != null)
                {
                    result.UnExpiredCoupons = unExpiredList.Select(p => new CouponDTO()
                    {
                        title = p.Coupon.Title,
                        subTitle = p.Coupon.SubTitle,
                        beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
                        couponId = p.CouponId,
                        couponType = p.Coupon.Type.CouponTypeName,
                        endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
                        couponUsageId = p.CouponUsageId
                    }).ToList();
                }
                var usedList = _repository.GetUsedCoupons(user);
                if (usedList != null)
                {
                    result.UsedCoupons = usedList.Select(p => new CouponDTO()
                    {
                        title = p.Coupon.Title,
                        subTitle = p.Coupon.SubTitle,
                        beginTime = p.Coupon.BeginTime.ToString("yyyy-MM-dd"),
                        couponId = p.CouponId,
                        couponType = p.Coupon.Type.CouponTypeName,
                        endTime = p.Coupon.EndTime.ToString("yyyy-MM-dd"),
                        usedTime = p.UsedTime == null ? string.Empty
                          : p.UsedTime.Value.ToString("yyyy-MM-dd"),
                        couponUsageId = p.CouponUsageId
                    }).ToList();
                }


            }

            return result;
        }

        /// <summary>
        /// 获取随机优惠券
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public CouponDTO GetRandomCoupon(UserInfoDTO wxUser)
        {
            CouponDTO result = null;
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
            //   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            //{
                if (!BeParticipatedInCoupon(wxUser))
            {



                


             
                    var availableCoupons = _repository.GetAvailableCoupon();

                if (availableCoupons.Any())
                {

                    Infrastructure.DomainDataAccess.Coupon.Entitys.Coupon coupon =
                        _repository.GetRandomCoupon(availableCoupons);

                    if (coupon != null)
                    {
                        //保存微信用户信息
                        var user = new UserInfo()
                        {
                            openid = wxUser.openid,
                            groupid = wxUser.groupid,
                            headimgurl = wxUser.headimgurl,
                            language = wxUser.language,
                            province = wxUser.province,
                            remark = wxUser.remark,
                            subscribe = wxUser.subscribe.ToString(),
                            subscribe_time = wxUser.subscribe_time,
                            nickname = wxUser.nickname,
                            sex = wxUser.sex,
                            city = wxUser.country,
                            country = wxUser.country
                        };
                        _userRepository.SaveUser(user);

                        //摇到就立即领取,如空优惠券
                        if (coupon.ShakeToReceive)
                        {
                            AddCouponToUser(user.openid, coupon.CouponId);
                            result = TransformCoupon(coupon, CouponState.HasReceived);
                        }
                        else
                        {
                            result = TransformCoupon(coupon);

                        }



                    }
                }
                else
                {
                    result = _noCoupon;
                }
                   


            }
            else
            {
                result = _noCoupon;
            }
            //    scope.Complete();
            //}
            return result;
        }

        public void AddCouponToUser(string openid, Guid couponId)
        {
            if (couponId != null && !string.IsNullOrEmpty(openid))
            {

                var coupon = _repository.GetCoupon(couponId);

                _repository.AddCouponToUser(openid, coupon);
            }
        }

        /// <summary>
        /// 判断是否参与过本次摇奖
        /// </summary>
        /// <returns></returns>
        public bool BeParticipatedInCoupon(UserInfoDTO wxUser)
        {
            bool result = false;

            result = _repository.BeParticipatedInCoupon(wxUser.openid);

            return result;
        }



        /// <summary>
        /// 判断是否还是有优惠券
        /// </summary>
        /// <returns></returns>
        public bool HasCoupon()
        {
            bool result = false;

            result = _repository.HasCoupon();

            return result;
        }



        /// <summary>
        /// 获取指定优惠券,默认未领取
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="coupon"></param>
        /// <returns></returns>
        private CouponDTO TransformCoupon(Infrastructure.DomainDataAccess.Coupon.Entitys.Coupon coupon

                , CouponState status = CouponState.NotReceived)
        {
            CouponDTO result = null;

            if (coupon != null)
            {
                result = new CouponDTO()
                {
                    couponId = coupon.CouponId,
                    //openId = user.OpenId,
                    title = coupon.Title,
                    subTitle = coupon.SubTitle,
                    beginTime = coupon.BeginTime.ToString("yyyy-MM-dd"),
                    endTime = coupon.EndTime.ToString("yyyy-MM-dd"),
                    getTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    couponType = coupon.Type.CouponTypeName,
                    status = status

                };
            }

            return result;
        }

        private CouponDTO TransformCoupon(CouponUsage coupon)
        {
            CouponDTO result = null;

            if (coupon != null)
            {
                result = new CouponDTO()
                {
                    couponId = coupon.CouponId,
                    //openId = user.OpenId,
                    title = coupon.Coupon.Title,
                    subTitle = coupon.Coupon.SubTitle,
                    beginTime = coupon.Coupon.BeginTime.ToString("yyyy-MM-dd"),
                    endTime = coupon.Coupon.EndTime.ToString("yyyy-MM-dd"),
                    getTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    couponType = coupon.Coupon.Type.CouponTypeName,
                    status = coupon.State,
                    usedTime = coupon.UsedTime == null ? string.Empty :
                    coupon.UsedTime.Value.ToString("yyyy-MM-dd")
                };
            }

            return result;
        }

        public void UseCoupon(string openId, Guid id)
        {
            if (!string.IsNullOrEmpty(openId))
            {
                _repository.UseCoupon(openId, id);
            }
        }
    }
}
