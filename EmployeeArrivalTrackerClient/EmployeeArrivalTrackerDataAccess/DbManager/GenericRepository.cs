using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeArrivalTrackerDataAccess.DbManager
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EmployeeArrivalContext context;
        private DbSet<T> table;

        public GenericRepository(EmployeeArrivalContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
            IQueryable<T> query = this.context.Set<T>();
            return query;
        }

        public IQueryable<T> GetQueryableByQuery(string query)
        {
            IQueryable<T> queryableResult = this.context.Set<T>().FromSqlRaw(query);
            return queryableResult;
        }

        public IEnumerable<T> GetAll()
        {
            return this.table.ToList();
        }

        public T GetFirstOrDefault(
            Expression<Func<T,bool>> filter = null, 
            params Expression<Func<T,object>>[] includeProperties)
        {
            IQueryable<T> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public T GetById(object id)
        {
            return this.table.Find(id);
        }

        public void Insert(T obj)
        {
            this.table.Add(obj);
        }

        public void InsertAll(IEnumerable<T> entities)
        {
            this.table.AddRange(entities);
        }

        public void Update(T obj)
        {
            this.table.Attach(obj);
            this.context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = this.table.Find(id);
            this.table.Remove(existing);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
