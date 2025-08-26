using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllLeave")]
        public async Task<IActionResult> GetAllLeave() 
        {
            var result =await _unitOfWork.Leave.GetAllAsync();
            return Ok(result);
        
        }
        [HttpGet("GetLeaveById/{id}")]
        public async Task<IActionResult> GetLeaveById(int id)
        {
            var result = await _unitOfWork.Leave.GetByIdAsync(id);
            return Ok(result);

        }
        [HttpPost("AddLeave")]
        public async Task<IActionResult> AddLeave(LeaveDTO leaveDTO)
        {
            var result =await _unitOfWork.Leave.AddAsync(leaveDTO);
            return Ok(result);
        }

        [HttpPut("ApproveLeave/{id}")]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            var result = await _unitOfWork.Leave.ApproveAsync(id);
            return Ok(result);
        }
        [HttpPut("RejectLeave/{id}")]
        public async Task<IActionResult> RejectLeave(int id)
        {
            var result = await _unitOfWork.Leave.RejectAsync(id);
            return Ok(result);
        }
        [HttpPut("UpdateOnLeave/{id}")]
        public async Task<IActionResult> UpdateOnLeave(LeaveDTO leaveDTO, int id)
        {
            var result = await _unitOfWork.Leave.UpdateAsync(leaveDTO, id);
            return Ok(result);
        }
        [HttpDelete("DeleteLeave/{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var result = await _unitOfWork.Leave.DeleteAsync(id);
            return Ok(result);
        }

    }
}
