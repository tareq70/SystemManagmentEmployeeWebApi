using SystemManagmentEmployeeWebApi.DTOs;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface IEmployeeRepository
    {
        //Get All
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync();

        //Get by Id
        Task<EmployeeDTO?> GetEmployeeByIdAsync(int id);
        
        //Create
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDTO);

        //Update
        Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employeeDTO, int id);

        //Delete
        Task<bool> DeleteEmployeeByIdAsync(int id);
    }
}
