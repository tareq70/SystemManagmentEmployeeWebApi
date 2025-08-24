using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendenceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("CheckIn/{id}")]
        public async Task<IActionResult> CheckIn(int id)
        {
            var result =await _unitOfWork.Attendance.CheckInAsync(id);

            return Ok(result);

        }
        [HttpPut("CheckOut/{id}")]
        public async Task<IActionResult> CheckOut(int id)
        {
            var result = await _unitOfWork.Attendance.CheckOutAsync(id);

            return Ok(result);

        }
        [HttpGet("GetAllAttendance")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var result =await _unitOfWork.Attendance.GetAllAttendanceAsync();
            return Ok(result);

        }
        [HttpGet("GetAttendanceByEmployee/{id}")]

        public async Task<IActionResult> GetAttendanceByEmployee(int id)
        {
            var result =await _unitOfWork.Attendance.GetAttendanceByEmployeeAsync(id);
            return Ok(result);

        }






    }
}
