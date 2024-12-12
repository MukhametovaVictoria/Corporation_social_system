using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}