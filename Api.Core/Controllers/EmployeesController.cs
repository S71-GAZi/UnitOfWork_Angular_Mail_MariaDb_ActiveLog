using Api.Core.Core.IRepository;
using Api.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _logger;

        private readonly IUnitOfWork _unitOfWork;
       

        public EmployeesController(
           
                 ILogger<EmployeesController> logger,   
                 IUnitOfWork unitOfWork)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Employees.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _unitOfWork.Employees.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(Employee employee)
        {
            if (ModelState.IsValid)
            {
                //employee.EmployeeId = Guid.NewGuid();

                await _unitOfWork.Employees.Add(employee);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { employee.EmployeeId}, employee);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        //Update//////
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
                return BadRequest();

            //await _unitOfWork.Employees.Update(x=>x.EmployeeId==id);
            await _unitOfWork.Employees.Update(employee);

            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _unitOfWork.Employees.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Employees.Delete(x=> x.EmployeeId == id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
