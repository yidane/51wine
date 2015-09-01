namespace Travel.Infrastructure.DomainDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Coupon.Entitys;

    using Travel.Infrastructure.DomainDataAccess.Order;

    using User;

    public class TravelDBContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“TravelDBContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“Travel.Infrastructure.DomainDataAccess.TravelDBContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“TravelDBContext”
        //连接字符串。
        public TravelDBContext()
            : base("name=DbConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable("tb_" + entity.ClrType.Name));
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Coupon.Entitys.Coupon> Coupon { get; set; }

        public virtual DbSet<CouponType> CouponType { get; set; }

        public virtual DbSet<UserGroup> UserGroup { get; set; }

        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public virtual DbSet<CouponUsage> CouponUsage { get; set; }

        public virtual DbSet<OrderEntity> Order { get; set; }

        public virtual DbSet<OrderDetailEntity> OrderDetail { get; set; }

        public virtual DbSet<TicketEntity> Ticket { get; set; }

        public virtual DbSet<TicketCategoryEntity> TicketCategory { get; set; }

        public virtual DbSet<DateTicketEntity> DateTicket { get; set; }

        public virtual DbSet<RefundOrderQueueEntity> RefundOrderQueue { get; set; }

        public virtual DbSet<RefundOrderEntity> RefundOrder { get; set; }

        public virtual DbSet<UserContracts> UserContract { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}