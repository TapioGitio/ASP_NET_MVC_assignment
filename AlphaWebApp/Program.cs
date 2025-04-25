using AlphaWebApp.Hubs;
using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(); 
builder.Services.Configure<CookiePolicyOptions>(x =>
{
    x.CheckConsentNeeded = context => !context.Request.Cookies.ContainsKey("cookieConsent");
    x.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("AlphaConnection")));

/* Services */
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

/* Repositories */
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
    x.Cookie.SameSite = SameSiteMode.None;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie()
.AddGoogle(x =>
{
    x.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    x.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
});
var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
using (var createAdminAtStartup = app.Services.CreateScope())
{
    var roleManager = createAdminAtStartup.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["Admin", "User"];

    foreach (string role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var UserManager = createAdminAtStartup.ServiceProvider.GetRequiredService<UserManager<MemberEntity>>();
    var user = new MemberEntity { UserName = "admin@domain.com", Email = "admin@domain.com", FirstName = "Mr", LastName = "Admin" };
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
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
