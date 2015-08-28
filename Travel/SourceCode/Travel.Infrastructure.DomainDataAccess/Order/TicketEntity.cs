namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TicketEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketId { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity Order { get; set; }

        /// <summary>
        /// 票种Id
        /// </summary>
        public Guid TicketCategoryId { get; set; }

        /// <summary>
        /// 票编号
        /// </summary>
        [Required, MaxLength(50)]
        public string TicketCode { get; set; }

        /// <summary>
        /// 票状态
        /// </summary>
        [Required, MaxLength(20)]
        public string TicketStatus { get; set; }

        /// <summary>
        /// 电子码
        /// </summary>
        [MaxLength(50)]
        public string ECode { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LatestModifyTime { get; set; }

        /// <summary>
        /// 游玩开始时间
        /// </summary>
        public DateTime TicketStartTime { get; set; }

        /// <summary>
        /// 游玩结束时间
        /// </summary>
        public DateTime TicketEndTime { get; set; }
    }
}
