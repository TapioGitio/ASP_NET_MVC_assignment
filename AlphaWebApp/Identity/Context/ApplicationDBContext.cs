using AlphaWebApp.Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlphaWebApp.Identity.Context;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
}

