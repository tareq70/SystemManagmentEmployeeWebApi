using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IAttendanceRepository
    {
        // CheckInAsync
        Task<AttendanceDTO> CheckInAsync(int employeeId);


        // CheckOutAsync
        Task<AttendanceDTO> CheckOutAsync(int employeeId);


        // GetAttendanceByEmployeeAsync
        Task<IEnumerable<AttendanceDTO>> GetAttendanceByEmployeeAsync(int employeeId);


        // GetAllAttendanceAsync
        Task<IEnumerable<AttendanceDTO>> GetAllAttendanceAsync();



    }
}
