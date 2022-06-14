using Api.Core.Core.IRepository;
using Api.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Core.Core.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EmployeeDbContext _context;
        private readonly ILogger _logger;

        public IEmployeeRepository Employees { get; private set; }

//IEmployeeRepository IUnitOfWork.Employees => throw new NotImplementedException();

        public UnitOfWork(EmployeeDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
             Employees = new EmployeeRepository(_context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
