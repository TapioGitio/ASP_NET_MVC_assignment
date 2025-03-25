using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("AlphaConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();







builder.Services.AddIdentity<MemberEntity, IdentityRole>(x =>
    {
        x.Password.RequiredLength = 8;
        x.Password.RequireNonAlphanumeric = false;
        x.User.RequireUniqueEmail = true;
        x.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/login";
    x.AccessDeniedPath = "/accessdenied";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    x.SlidingExpiration = true;
    x.Cookie.HttpOnly = true; 
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
using (var createAdminAtStartup = app.Services.CreateScope())
{
    var roleManager = createAdminAtStartup.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };

    foreach (string role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var UserManager = createAdminAtStartup.ServiceProvider.GetRequiredService<UserManager<MemberEntity>>();
    var user = new MemberEntity { UserName = "admin@domain.com", Email = "admin@domain.com" };
    var userExists = await UserManager.Users.AnyAsync(x => x.Email == user.Email);
    if (!userExists)
    {
        var result = await UserManager.CreateAsync(user, "Bytmig123");
        if (result.Succeeded)
        {
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
