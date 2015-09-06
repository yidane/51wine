namespace Travel.Infrastructure.DomainDataAccess.Coupon.Entitys
{
    public enum CouponState
    {
        /// <summary>
        /// 未使用
        /// </summary>
        NotUsed=0,
        /// <summary>
        /// 已使用
        /// </summary>
        Used,
        /// <summary>
        /// 已参与抽奖
        /// </summary>
        BeParticipatedIn,
        /// <summary>
        /// 未领取
        /// </summary>
        NotReceived,
        /// <summary>
        /// 已领取
        /// </summary>
        HasReceived
    }
}