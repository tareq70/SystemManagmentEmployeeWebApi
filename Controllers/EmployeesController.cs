using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result =await _unitOfWork.Employee.GetAllEmployeeAsync();

            return Ok(result);
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _unitOfWork.Employee.GetEmployeeByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employeeDTO)
        {
            var result = await _unitOfWork.Employee.AddEmployeeAsync(employeeDTO);
            return Ok(result);
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO employeeDTO, int id)
        {
            var result = await _unitOfWork.Employee.UpdateEmployeeAsync(employeeDTO, id);
            return Ok(result);
        }
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _unitOfWork.Employee.DeleteEmployeeByIdAsync(id);
            return Ok(result);
        }

    }
}
