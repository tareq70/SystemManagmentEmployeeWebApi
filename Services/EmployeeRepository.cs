using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _Context;

        public EmployeeRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDTO)
        {

            var Employee = new Employee
            {
                FullName = employeeDTO.FullName,
                HireDate = employeeDTO.HireDate,
                Salary = employeeDTO.Salary,
                DepartmentId = employeeDTO.DepartmentId,
                Age = employeeDTO.Age,
                Address = employeeDTO.Address,
                Email = employeeDTO.Email,
                Phone = employeeDTO.Phone
            };
            await _Context.Employees.AddAsync(Employee);
            await _Context.SaveChangesAsync();

            var department = await _Context.Departments.FindAsync(Employee.DepartmentId);

            return new EmployeeDTO
            {
                Id = Employee.Id,
                FullName = Employee.FullName,
                HireDate = Employee.HireDate,
                Salary = Employee.Salary,
                DepartmentName = department != null ? department.Name : string.Empty,
                Age = Employee.Age,
                Address = Employee.Address,
                Email = Employee.Email,
                Phone = Employee.Phone
            };

        }

        public async Task<bool> DeleteEmployeeByIdAsync(int id)
        {
            var employee = await _Context.Employees.FindAsync(id);
            if (employee != null)
                _Context.Employees.Remove(employee);


            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync()
        {
            var Employees = await _Context.Employees
                .Include(e => e.Department)
                .Select(x => new EmployeeDTO
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    HireDate = x.HireDate,
                    Salary = x.Salary,
                    DepartmentName = x.Department != null ? x.Department.Name : string.Empty,
                    Age = x.Age,
                    Address = x.Address,
                    Email = x.Email,
                    Phone = x.Phone

                }).ToListAsync();
            return Employees;
        }

        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _Context.Employees
                .Include(x => x.Department)
                .Where(x => x.Id == id)
                .Select(x => new EmployeeDTO
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    HireDate = x.HireDate,
                    Salary = x.Salary,
                    DepartmentName = x.Department != null ? x.Department.Name : string.Empty,
                    Age = x.Age,
                    Address = x.Address,
                    Email = x.Email,
                    Phone = x.Phone
                }).FirstOrDefaultAsync();

            return employee;
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employeeDTO, int id)
        {
            var Emp = await _Context.Employees.FindAsync(id);
            if (Emp is null)
                return null;
  

            Emp.FullName = employeeDTO.FullName;
            Emp.HireDate = employeeDTO.HireDate;
            Emp.Salary = employeeDTO.Salary;
            Emp.DepartmentId = employeeDTO.DepartmentId;
            Emp.Address = employeeDTO.Address;
            Emp.Email = employeeDTO.Email;
            Emp.Phone = employeeDTO.Phone;
            _Context.Employees.Update(Emp);
            await _Context.SaveChangesAsync();
            return new EmployeeDTO
            {
                Id = Emp.Id,
                FullName = Emp.FullName,
                HireDate = Emp.HireDate,
                Salary = Emp.Salary,
                DepartmentId = Emp.DepartmentId,
                Age = Emp.Age,
                Address = Emp.Address,
                Email = Emp.Email,
                Phone = Emp.Phone
            };
        }
    }
}
