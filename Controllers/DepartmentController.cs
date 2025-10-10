using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _unitOfWork.Departments.GetAllDepartmentsAsync();
            return Ok(result);
        }

        [HttpGet("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] int id)
        {
            var result = await _unitOfWork.Departments.GetDepartmentByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return Ok(result);

        }
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            var result =await _unitOfWork.Departments.CreateDepartmentAsync(departmentDTO);
            await _unitOfWork.CompleteAsync();

            return Ok(result);
        }

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromQuery] int id,[FromBody] DepartmentDTO departmentDTO) 
        {
            var result = await _unitOfWork.Departments.UpdateDepartment(id, departmentDTO);
            await _unitOfWork.CompleteAsync();

            return Ok(result);
        
        }
        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromQuery] int id)
        {
            var result = await _unitOfWork.Departments.DeleteDepartment(id);
            await _unitOfWork.CompleteAsync();

            return Ok(result);

        }

    }
}
