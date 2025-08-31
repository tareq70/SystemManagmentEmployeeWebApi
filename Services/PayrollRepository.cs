using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class PayrollRepository : IPayrollRepository
    {
        AppDbContext context;

        public PayrollRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PayrollDTO>> GetAllAsync()
        {
            return await context.Payrolls
                .Include(p => p.Employee)

                .Select(p => new PayrollDTO
                {
                    EmployeeId = p.EmployeeId,
                    Month = p.Month,
                    NetSalary = p.NetSalary,
                    IsPaid = p.IsPaid,
                    TransactionId = p.TransactionId,
                    EmpName = p.Employee != null ? p.Employee.FullName : string.Empty
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PayrollDTO>> GetByEmployeeAsync(int employeeId)
        {
            return await context.Payrolls
                 .Include(p => p.Employee)
                 .Where(p => p.EmployeeId == employeeId)
                 .Select(p => new PayrollDTO
                 {
                     EmployeeId = p.EmployeeId,
                     Month = p.Month,
                     NetSalary = p.NetSalary,
                     IsPaid = p.IsPaid,
                     TransactionId = p.TransactionId,
                     EmpName = p.Employee != null ? p.Employee.FullName : string.Empty
                 }).ToListAsync();
        }

        public async Task<PayrollDTO> GeneratePayrollAsync(int employeeId, DateTime month)
        {
            var employee = await context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("Invalid employee ID");
            }

            var payrollDto = new Payroll
            {
                EmployeeId = employeeId,
                NetSalary = employee.Salary,
                Month = month,
                IsPaid = false
            };

            await context.Payrolls.AddAsync(payrollDto);
            await context.SaveChangesAsync();

            return new PayrollDTO
            {
                EmployeeId = payrollDto.EmployeeId,
                Month = payrollDto.Month,
                NetSalary = payrollDto.NetSalary,
                IsPaid = payrollDto.IsPaid,
                TransactionId = payrollDto.TransactionId,
                EmpName = employee.FullName
            };

        }

        public async Task<bool> PaySalaryAsync(int payrollId)
        {
            var payroll = await context.Payrolls.FindAsync(payrollId);
            if (payroll == null || payroll.IsPaid) return false;

            payroll.TransactionId = Guid.NewGuid().ToString();
            payroll.IsPaid = true;

            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<PayrollDTO>> GetPayrollForOneMonth(DateTime month)
        {
            return await context.Payrolls
                .Include(p => p.Employee)
                 .Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month)
                .Select(p => new PayrollDTO
                {
                    EmpName = p.Employee != null ? p.Employee.FullName : string.Empty,
                    Month = month,
                    NetSalary = p.NetSalary,
                    IsPaid = p.IsPaid,
                    TransactionId = p.TransactionId
                }).ToListAsync();
        }
    }
}
