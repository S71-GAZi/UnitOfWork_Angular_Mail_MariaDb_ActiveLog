using Api.Core.Models;
using System.Threading.Tasks;

namespace Api.Core.Core.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        Task CompleteAsync();
        
    }
}
