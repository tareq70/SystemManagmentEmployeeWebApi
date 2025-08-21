using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IDepartmentRepository
    {
        // Get All
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();

        // Get By Id
        Task<DepartmentDTO?> GetDepartmentByIdAsync(int id);

        // Create
        Task<DepartmentDTO> CreateDepartmentAsync(DepartmentDTO departmentDto);

        // Update
        Task<bool> UpdateDepartment(int id, DepartmentDTO dto);

        // Delete
        Task<bool> DeleteDepartment(int id);
    }

}
