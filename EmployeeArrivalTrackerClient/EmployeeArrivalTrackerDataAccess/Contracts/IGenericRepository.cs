using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T obj);

        void Update(T obj);

        void Delete(object id);

        void Save();
    }
}
