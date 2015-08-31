namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderDetailEntity
    {
        [Key]
        public Guid OrderDetailId { get; set; }

        /// <summary>
        /// 标识该详情是买票类还是退票类或是其它扩展类型
        /// </summary>
        public Guid OrderDetailCategoryId { get; set; }


        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity Order { get; set; }

        /// <summary>
        /// 票种Id
        /// </summary>
        public Guid TicketCategoryId { get; set; }

        /// <summary>
        /// 某种票的购买数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 某种票的单价
        /// </summary>
        public decimal SingleTicketPrice { get; set; }

        /// <summary>
        /// 是否有折扣
        /// </summary>
        public bool IsDiscount { get; set; }

        /// <summary>
        /// 折扣类型Id
        /// </summary>
        public Guid? DiscountCategoryId { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        public decimal TotalFee()
        {
            return this.SingleTicketPrice * this.Count;
        }

        public void Add()
        {
            using (var db = new TravelDBContext())
            {
                db.OrderDetail.Add(this);
                db.SaveChanges();
            }
        }
    }
}
