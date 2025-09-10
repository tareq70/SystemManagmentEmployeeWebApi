using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.DTOs;
using SystemManagmentEmployeeWebApi.Exceptions;
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
            var result = await _context.Leaves.Include(l => l.Employee)
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

            if (result is not null)
                return result;
            else
                throw new NotFoundException($"No Data Found..");
        }

        public async Task<IEnumerable<LeaveDTO?>> GetByIdAsync(int EmpId)
        {
            var result = await _context.Leaves
                .Include(l => l.Employee)
                .Where(l => l.EmployeeId == EmpId)
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
            if (result is not null)
                return result;
            else
                throw new NotFoundException($"No Data Found for Employee Id {EmpId}");
        }

        public async Task<LeaveDTO> AddAsync(LeaveDTO leaveDto)
        {
            var leave = new Leave
            {
                Id= leaveDto.Id,
                EmployeeId = leaveDto.EmployeeId,
                StartDate = leaveDto.StartDate.Date,
                EndDate = leaveDto.EndDate.Date,
                Reason = leaveDto.Reason,
                Status = "Pending"
            };

            

            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();

            var employee = await _context.Employees.FindAsync(leave.EmployeeId);


            return new LeaveDTO
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status,
                EmpName= employee.FullName

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
                throw new NotFoundException($"Leave with Id {leaveId} was not found.");

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
