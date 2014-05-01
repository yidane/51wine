using System.Collections.Generic;
using Wine.Util;

namespace Wine.Infrastructure.DAO
{
    internal abstract class BaseDAO<T>
    {
        protected string DBConnection = ConfigManager.DBConnection;

        public abstract T Insert(T t);
        public abstract List<T> Query(T t);
        public abstract void Delete(T t);
        public abstract void Update(T t);
    }
}