using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Message
{
    public interface IShortMsgRepository
    {
        void Add(ShortMsg entity);
        void Modify(ShortMsg entity);
        void Delete(int entityId);
        void Delete(Expression<Func<ShortMsg, bool>> predicate);
        IQueryable<ShortMsg> GetAllList(Expression<Func<ShortMsg, bool>> predicate);
        IQueryable<ShortMsg> GetAllList();
        ShortMsg Get(int id);
        void Delete(ShortMsg entity);
        void DeleteList(List<ShortMsg> list);
        void SaveChange();
        void Modify(IQueryable<ShortMsg> shortMsgs);
    }
}