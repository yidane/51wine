using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess
{
    using System.Data.Entity;
    using System.Transactions;

    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EFRepository(DbContext dbContext)
        {
            this.Context = dbContext;
        }

        public DbContext Context { get; protected set; }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            var state = this.Context.Entry(entity).State;

            if (state == EntityState.Detached)
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }

            this.Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            this.Context.Set<TEntity>().Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;

            return this.Context.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool Update(IEnumerable<TEntity> entities)
        {
            try
            {
                this.Context.Configuration.AutoDetectChangesEnabled = false;

                using (var scope = new TransactionScope())
                {
                    foreach (var entity in entities)
                    {
                        this.Update(entity);
                    }

                    scope.Complete();
                }
                
                return true;
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IList<TEntity> Get(Func<TEntity, bool> conditions)
        {
            if (conditions != null)
                return this.Context.Set<TEntity>().Where<TEntity>(conditions).ToList();
            else
                return this.Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 带分页的查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IList<TEntity> Get(int pageIndex, int pageSize, Func<TEntity, bool> conditions)
        {
            var skinCount = (pageIndex - 1) * pageSize;
            if (conditions != null)
                return this.Context.Set<TEntity>()
                    .Where<TEntity>(conditions)
                    .Skip(skinCount)
                    .Take(pageSize)
                    .ToList();
            else
                return this.Context.Set<TEntity>()
                    .Skip(skinCount)
                    .Take(pageSize)
                    .ToList();
        }
        
    }
}
