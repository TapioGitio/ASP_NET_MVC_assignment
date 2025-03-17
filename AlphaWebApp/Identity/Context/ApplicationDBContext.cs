using AlphaWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlphaWebApp.Identity.Context;

public class ApplicationDBContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{

}
