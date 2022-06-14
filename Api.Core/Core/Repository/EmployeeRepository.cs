using Api.Core.Core.IRepository;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Core.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext _context, ILogger logger) : base(_context, logger)
        {
        }



        /// <summary>
        /// ///
        

        public  override async Task<bool> Update(Employee entity)
        {
            try
            {
                var existingEmp = await dbSet.Where(x => x.EmployeeId == entity.EmployeeId)
                                                    .FirstOrDefaultAsync();

                if (existingEmp == null)
                    return await Add(entity);

                existingEmp.Name = entity.Name;
                existingEmp.Phone = entity.Phone;
                existingEmp.Email = entity.Email;
                existingEmp.Address = entity.Address;


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(EmployeeRepository));
                return false;
            }
        }



    }
}
