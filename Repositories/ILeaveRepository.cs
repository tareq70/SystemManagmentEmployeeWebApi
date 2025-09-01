using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.Repositories
{
    public interface ILeaveRepository
    {
        Task<IEnumerable<LeaveDTO>> GetAllAsync();
        Task<IEnumerable< LeaveDTO?>> GetByIdAsync(int Empid);

        Task<LeaveDTO> AddAsync(LeaveDTO leaveDto);
        Task<LeaveDTO> UpdateAsync(LeaveDTO leaveDto, int EmpId);  
        Task<bool> DeleteAsync(int Empid);       
       
        
        Task<bool> ApproveAsync(int Empid);
        Task<bool> RejectAsync(int Empid);
    }
}

