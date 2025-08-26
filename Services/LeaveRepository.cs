using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;

namespace SystemManagmentEmployeeWebApi.Services
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly AppDbContext _context;
        public LeaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveDTO>> GetAllAsync()
        {
            return await _context.Leaves.Include(l => l.Employee)
                .Select(l => new LeaveDTO
                {
                    Id = l.Id,
                    EmpName = l.Employee.FullName,
                    EmployeeId = l.EmployeeId,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Reason = l.Reason,
                    Status = l.Status
                }).ToListAsync();
        }

        public async Task<LeaveDTO?> GetByIdAsync(int leaveId)
        {
            return await _context.Leaves
                .Include(l => l.Employee)
                .Where(l => l.Id == leaveId)
                .Select(l => new LeaveDTO
                {
                    Id = l.Id,
                    EmpName = l.Employee.FullName,
                    EmployeeId = l.EmployeeId,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Reason = l.Reason,
                    Status = l.Status
                }).FirstOrDefaultAsync();
        }

        public async Task<LeaveDTO> AddAsync(LeaveDTO leaveDto)
        {
            var leave = new Leave
            {
                EmployeeId = leaveDto.EmployeeId,
                StartDate = leaveDto.StartDate,
                EndDate = leaveDto.EndDate,
                Reason = leaveDto.Reason,
                Status = "Pending"
            };

            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();

            return new LeaveDTO
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status,
                EmpName= leave.Employee.FullName

            };
        }

        public async Task<bool> ApproveAsync(int leaveId)
        {
            var leave = await _context.Leaves.FirstOrDefaultAsync(l => l.Id == leaveId && l.Status == "Pending");

            if (leave == null)
                return false;

            leave.Status = "Approved";
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAsync(int leaveId)
        {
            var leave = await _context.Leaves.FirstOrDefaultAsync(l => l.Id == leaveId);

            if (leave == null)
                return false;

            leave.Status = "Rejected";
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int leaveId)
        {
            var leave = await _context.Leaves.FirstOrDefaultAsync(l => l.Id == leaveId && l.Status == "Pending");

            if (leave == null)
                return false;

            _context.Leaves.Remove(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LeaveDTO?> UpdateAsync(LeaveDTO leaveDto, int leaveId)
        {
            var leave = await _context.Leaves.FindAsync(leaveId);

            if (leave == null)
                return null;

            leave.StartDate = leaveDto.StartDate;
            leave.EndDate = leaveDto.EndDate;
            leave.Reason = leaveDto.Reason;
            leave.Status = "Pending";

            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();

            return new LeaveDTO
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status
            };
        }
    }

}
