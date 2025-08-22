using Microsoft.AspNetCore.Mvc;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employee { get; }

        Task<int> CompleteAsync();


    }
}
