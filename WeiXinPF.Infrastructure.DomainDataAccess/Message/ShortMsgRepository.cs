using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Message
{
    public class ShortMsgRepository : IShortMsgRepository
    {
        private readonly WXDBContext _context;
        public ShortMsgRepository(WXDBContext context)
        {
            _context = context;
        }
        public void Add(ShortMsg entity)
        {
             
//            _context.ShortMsg.Add(entity);
//            _context.SaveChanges();
        }

        public void Modify(ShortMsg entity)
        {
//            _context.ShortMsg.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
//            _context.SaveChanges();
        }

        public void Delete(int entityId)
        {
            var entity = Get(entityId);
//            _context.ShortMsg.Remove(entity);
//            _context.SaveChanges();
        }

        public void Delete(Expression<Func<ShortMsg, bool>> predicate)
        {
            var systementitys = GetAllList(predicate);
//            _context.ShortMsg.RemoveRange(systementitys);
        }

        public IQueryable<ShortMsg> GetAllList(Expression<Func<ShortMsg, bool>> predicate)
        {
//            return _context.ShortMsg.Where(predicate);

            return null;
        }

        public IQueryable<ShortMsg> GetAllList()
        {
//                        return _context.ShortMsg;
            return null;
        }

        public ShortMsg Get(int id)
        {
            //                        return _context.ShortMsg.SingleOrDefault(b => b.Id == id);
            return null;
        }

        public void Delete(ShortMsg entity)
        {
//            _context.ShortMsg.Remove(entity);
            //            _context.SaveChanges();
           
        }

        public void DeleteList(List<ShortMsg> list)
        {
//                        _context.ShortMsg.RemoveRange(list);
            //            _context.SaveChanges();
            
        }

        public void SaveChange()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
           
        }

        public void Modify(IQueryable<ShortMsg> shortMsgs)
        {
            shortMsgs.ToList().ForEach(c =>
            {
               
//                _context.ShortMsg.Attach(c);
                _context.Entry(c).State = EntityState.Modified;
            })
            ;
             
        }
    }
}
