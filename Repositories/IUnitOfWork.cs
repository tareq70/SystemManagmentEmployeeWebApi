using Microsoft.AspNetCore.Mvc;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employee { get; }
        IAttendanceRepository Attendance {  get; }

        Task<int> CompleteAsync();


    }
}
