using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollDTO>> GetAllAsync();
        Task<IEnumerable<PayrollDTO>> GetByEmployeeAsync(int employeeId);
        Task<IEnumerable<PayrollDTO>> GetPayrollForOneMonth(DateTime month); 
        Task<PayrollDTO> GeneratePayrollAsync(int employeeId, DateTime month);
        //Task<PayrollDTO> GeneratePayrollAsync(DateTime month);
        Task<bool> PaySalaryAsync(int payrollId);
    }
}
