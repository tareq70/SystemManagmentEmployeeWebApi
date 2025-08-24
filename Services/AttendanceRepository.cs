using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceDTO> CheckInAsync(int employeeId)
        {
            var today = DateTime.UtcNow.Date;

            var checkexistingAttendance = await _context.Attendances
                .Include(A => A.Employee)
                  .Where(a => a.EmployeeId == employeeId && a.CheckIn.Date == today)
                .Select(A => new AttendanceDTO
                {
                    EmployeeId = A.EmployeeId,
                    EmpName = A.Employee.FullName,
                    CheckIn = A.CheckIn
                })
                .FirstOrDefaultAsync();

            if (checkexistingAttendance != null)
                return checkexistingAttendance;

            var Attend = new Attendance
            {
                EmployeeId = employeeId,
                CheckIn = DateTime.UtcNow
            };
            _context.Add(Attend);
            await _context.SaveChangesAsync();

            return new AttendanceDTO
            {
                EmployeeId = Attend.EmployeeId,
                EmpName = Attend.Employee?.FullName ?? string.Empty,
                CheckIn = Attend.CheckIn
            };
        }

        public async Task<AttendanceDTO> CheckOutAsync(int employeeId)
        {
            var today = DateTime.UtcNow.Date;

            var attendance = await _context.Attendances
         .Include(a => a.Employee)
         .FirstOrDefaultAsync(a => a.EmployeeId == employeeId
                                   && a.CheckIn.Date == today
                                   && a.CheckOut == null);

            if (attendance is null)
                return null;

            attendance.CheckOut = DateTime.UtcNow;
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();

            return new AttendanceDTO
            {
                EmployeeId = attendance.EmployeeId,
                EmpName = attendance.Employee.FullName,
                CheckIn = attendance.CheckIn,
                CheckOut = attendance.CheckOut,
                WorkingHours = attendance.WorkingHours
            };
        }

        public async Task<IEnumerable<AttendanceDTO>> GetAllAttendanceAsync()
        {
            var Attendances =await _context.Attendances
                .Include(a => a.Employee)
                .Select(A=> new AttendanceDTO
                {
                    EmpName = A.Employee.FullName,
                    CheckIn = A.CheckIn,
                    CheckOut = A.CheckOut,
                    WorkingHours = A.WorkingHours

                }).ToListAsync();

            return Attendances;
        }

        public Task<AttendanceDTO> GetAttendanceByEmployeeAsync(int employeeId)
        {
            var emp = _context.Attendances.Include(a => a.Employee)
                .Where(A=>A.EmployeeId == employeeId)
               .Select(A => new AttendanceDTO
               {
                   EmpName = A.Employee.FullName,
                   CheckIn = A.CheckIn,
                   CheckOut = A.CheckOut,
                   WorkingHours = A.WorkingHours

               }).FirstOrDefaultAsync();

            return emp;
        }
    }
}
