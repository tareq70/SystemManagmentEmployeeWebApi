using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(AppDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            Departments = departmentRepository;
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
