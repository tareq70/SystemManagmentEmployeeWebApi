using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employee { get; }
        public IAttendanceRepository Attendance { get; }
        public ILeaveRepository Leave { get; }
        public UnitOfWork(AppDbContext context, IDepartmentRepository departmentRepository, IEmployeeRepository employees, IAttendanceRepository attendances, ILeaveRepository leaveRepository)
        {
            _context = context;
            Departments = departmentRepository;
            Employee = employees;
            Attendance = attendances;
            Leave = leaveRepository;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
