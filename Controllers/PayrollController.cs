using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        IUnitOfWork _unitOfWork;

        public PayrollController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllPayrolls")]
        public async Task<IActionResult> GetAllPayrolls()
        {
            var payrolls = await _unitOfWork.PayrollRepository.GetAllAsync();
            return Ok(payrolls);
        }
        [HttpGet("GetByEmpId")]
        public async Task<IActionResult> GetByEmpId(int empId) 
        {
            var payrolls = await _unitOfWork.PayrollRepository.GetByEmployeeAsync(empId);
            return Ok(payrolls);
        }
        [HttpGet("GetByMonth")]
        public async Task<IActionResult> GetByMonth(DateTime dateTime)
        {
            var payrolls = await _unitOfWork.PayrollRepository.GetPayrollForOneMonth(dateTime);
            return Ok(payrolls);
        }



        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromQuery] int id ,[FromQuery] DateTime date)
        {
            var result = await _unitOfWork.PayrollRepository.GenerateAndPayPayrollAsync(id, date);

            if (result is null)
                return BadRequest("Payment failed.");
            return Ok("Payment successful.");
        }
    }
}
