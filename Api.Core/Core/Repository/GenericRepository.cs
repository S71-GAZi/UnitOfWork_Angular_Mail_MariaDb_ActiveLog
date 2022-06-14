using Api.Core.Core.IRepository;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Core.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected EmployeeDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;
        public GenericRepository(EmployeeDbContext _context, ILogger logger)
        {
            this.context = _context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }


        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }



        //public async Task<bool> Add(T entity)
        //{
        //    await dbSet.AddAsync(entity);
        //    return true;
        //}

        public async Task<IEnumerable<T>> All()
        {
            var all = await dbSet.ToListAsync();
            return all; 
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> predicate)
        {
            var obj =await dbSet.FirstOrDefaultAsync(predicate);
            if(obj != null)
            {
                dbSet.Remove(obj);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        //public async Task<bool> Update(Employee employee)
        //{
        //    var obj = await dbSet.Where(x=>x.);
        //    if (obj != null)
        //    {
        //        dbSet.Update(obj);
        //        return true;
        //    }
        //    return false;
        //}

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        //public Task<bool> Upsert(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
