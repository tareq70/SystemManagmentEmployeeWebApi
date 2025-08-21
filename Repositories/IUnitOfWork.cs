using Microsoft.AspNetCore.Mvc;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Departments { get; }

        Task<int> CompleteAsync();


    }
}
