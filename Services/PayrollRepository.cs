using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.Controllers.Fake_Api;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Exceptions;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class PayrollRepository : IPayrollRepository 
    {
        AppDbContext context;
        IBankService bankService;
        public PayrollRepository(AppDbContext context , IBankService bankService)
        {
            this.context = context;
            this.bankService = bankService;
        }

        public async Task<IEnumerable<PayrollDTO>> GetAllAsync()
        {
            var result = await context.Payrolls
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

            if (result is not null)
                return result;
            else
                throw new NotFoundException("No Data Found..");
        }

        public async Task<IEnumerable<PayrollDTO>> GetByEmployeeAsync(int employeeId)
        {
            var result = await context.Payrolls
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
            if (result is not null)
                return result;
            else
                throw new NotFoundException($"No Data Found for Employee Id {employeeId}");
        }

     
        public async Task<PayrollDTO?> GenerateAndPayPayrollAsync(int employeeId, DateTime month)
        {
            var employee = await context.Employees.FindAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with Id {employeeId} was not found.");

            // Step 1: Create payroll
            var payroll = new Payroll
            {
                EmployeeId = employeeId,
                NetSalary = employee.Salary,
                Month = month,
                IsPaid = false
            };

            await context.Payrolls.AddAsync(payroll);
            await context.SaveChangesAsync();

            // Step 2: Call Fake Bank API
            var result = await bankService.TransferSalaryAsync(
                employee.BankAccountNumber,
                payroll.NetSalary
            );

            if (result == null || !result.Success)
                return null;

            // Step 3: Update payroll
            payroll.TransactionId = result.TransactionId; // Transaction من البنك
            payroll.IsPaid = true;

            await context.SaveChangesAsync();

            // Step 4: Return DTO
            return new PayrollDTO
            {
                EmployeeId = payroll.EmployeeId,
                Month = payroll.Month,
                NetSalary = payroll.NetSalary,
                IsPaid = payroll.IsPaid,
                TransactionId = payroll.TransactionId,
                EmpName = employee.FullName
            };
        }


        public async Task<IEnumerable<PayrollDTO>> GetPayrollForOneMonth(DateTime month)
        {
            var result = await context.Payrolls
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
            if (result is not null)
                return result;
            else
                throw new NotFoundException($"No Data Found for Month {month.ToString("Y")}");
        }
    }
}
