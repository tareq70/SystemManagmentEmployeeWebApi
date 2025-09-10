using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Exceptions;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            var result = await _context.Departments
                .Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                }).ToListAsync();


            if (result is not null)
                return result;
            else
                throw new NotFoundException($"No Data Found..");
        }

        public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int id)
        {
            var result = await _context.Departments
                 .Where(X => X.Id == id)
                 .Select(Dept => new DepartmentDTO
                 {
                     Id = Dept.Id,
                     Name = Dept.Name,
                     Description = Dept.Description

                 }).FirstOrDefaultAsync();

            if (result is not null)
                return result;
            else
                throw new NotFoundException($"Department with Id {id} was not found.");
        }
        public async Task<DepartmentDTO> CreateDepartmentAsync(DepartmentDTO departmentDTO)
        {
            var department = new Department
            {
                Name = departmentDTO.Name,
                Description = departmentDTO.Description
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<bool> UpdateDepartment(int id, DepartmentDTO dto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) 
                throw new NotFoundException($"Department with Id {id} was not found.");


            department.Name = dto.Name;
            department.Description = dto.Description;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) 
                throw new NotFoundException($"Department with Id {id} was not found.");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
