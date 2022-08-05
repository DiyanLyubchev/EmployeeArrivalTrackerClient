using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetQueryable();

        IQueryable<T> GetQueryableByQuery(string query);

        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T obj);

        void InsertAll(IEnumerable<T> entities);

        void Update(T obj);

        void Delete(object id);

        void Save();
    }
}
