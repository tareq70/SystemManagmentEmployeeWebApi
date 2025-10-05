using Microsoft.AspNetCore.Identity;

namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = default!;



    }
}
