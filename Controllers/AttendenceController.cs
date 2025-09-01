using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendenceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn([FromQuery] int id)
        {
            var result = await _unitOfWork.Attendance.CheckInAsync(id);

            return Ok(result);

        }
        [HttpPut("CheckOut")]
        public async Task<IActionResult> CheckOut([FromQuery] int id)
        {
            var result = await _unitOfWork.Attendance.CheckOutAsync(id);

            return Ok(result);

        }
        [HttpGet("GetAllAttendance")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var result = await _unitOfWork.Attendance.GetAllAttendanceAsync();
            return Ok(result);

        }
        [HttpGet("GetAttendanceByEmployee")]

        public async Task<IActionResult> GetAttendanceByEmployee([FromQuery] int id)
        {
            var result = await _unitOfWork.Attendance.GetAttendanceByEmployeeAsync(id);
            return Ok(result);
        }
    }
}
