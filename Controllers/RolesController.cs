using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet("GetAllRoles")]
        [Authorize(Roles = "Manager")]
        public async Task< IActionResult> GetAllRoles()
        {
            try
            {
                var roles =await _roleManager.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }



        [HttpPost("AddRole")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required.");

            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
                return Ok($"Role '{roleName}' created successfully.");

            return BadRequest(result.Errors);
        }

    [HttpPost("AssignRole")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> AssignRoleToUser(string userEmail, string roleName)
    {
        if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(roleName))
            return BadRequest("Email and RoleName are required.");

        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            return NotFound("User not found.");

        if (!await _roleManager.RoleExistsAsync(roleName))
            return NotFound("Role not found.");

        if (await _userManager.IsInRoleAsync(user, roleName))
            return BadRequest("User already has this role.");

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (result.Succeeded)
            return Ok($"Role '{roleName}' assigned to user '{userEmail}' successfully.");

        return BadRequest(result.Errors);
    }
    }
}
