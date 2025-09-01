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
        [HttpGet("GetLeaveById")]
        public async Task<IActionResult> GetLeaveById([FromQuery] int Id)
        {
            var result = await _unitOfWork.Leave.GetByIdAsync(Id);
            return Ok(result);

        }
        [HttpPost("AddLeave")]
        public async Task<IActionResult> AddLeave(LeaveDTO leaveDTO)
        {
            var result =await _unitOfWork.Leave.AddAsync(leaveDTO);
            return Ok(result);
        }

        [HttpPut("ApproveLeave")]
        public async Task<IActionResult> ApproveLeave([FromQuery] int id)
        {
            var result = await _unitOfWork.Leave.ApproveAsync(id);
            return Ok(result);
        }
        [HttpPut("RejectLeave")]
        public async Task<IActionResult> RejectLeave([FromQuery]int id)
        {
            var result = await _unitOfWork.Leave.RejectAsync(id);
            return Ok(result);
        }
        [HttpPut("UpdateOnLeave")]
        public async Task<IActionResult> UpdateOnLeave(LeaveDTO leaveDTO,[FromQuery] int id)
        {
            var result = await _unitOfWork.Leave.UpdateAsync(leaveDTO, id);
            return Ok(result);
        }
        [HttpDelete("DeleteLeave")]
        public async Task<IActionResult> DeleteLeave([FromQuery] int id)
        {
            var result = await _unitOfWork.Leave.DeleteAsync(id);
            return Ok(result);
        }

    }
}
