using ContractManagementSystem.Authorization;
using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Listen on port 3000 for all network interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(3000);
});



// Add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Adjust as needed
        options.JsonSerializerOptions.WriteIndented = true; // Optional, for better readability
    });

// Check and create the templates directory
var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates");
if (!Directory.Exists(templatesPath))
{
    Directory.CreateDirectory(templatesPath);
}
// Register services
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<GetApproversService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ClauseService>();
builder.Services.AddScoped<ICollaborationService, CollaborationService>();
builder.Services.AddScoped<IReportService, ReportService>();

// Add DbContext and specify the connection string
// var connectionString = builder.Configuration.GetConnectionString("ContractManagementSystemContextConnection");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));

var connectionString = builder.Configuration.GetConnectionString("ContractManagementSystemContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));



// Add Identity services
builder.Services.AddIdentity<AppUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register the PrivilegeService
builder.Services.AddScoped<IPrivilegeService, PrivilegeService>();

// Register the RoleService
builder.Services.AddScoped<IRoleService, RoleService>();

// Register the Custom Authorization Handler as scoped
builder.Services.AddScoped<IAuthorizationHandler, PrivilegeHandler>();

// Define Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("InitiateContract", policy =>
        policy.Requirements.Add(new PrivilegeRequirement("InitiateContract")));
    options.AddPolicy("CreateUser", policy =>
        policy.Requirements.Add(new PrivilegeRequirement("CreateUser")));
    options.AddPolicy("DeleteUser", policy =>
        policy.Requirements.Add(new PrivilegeRequirement("DeleteUser")));
    // Add more policies as needed
});

// Configure Identity password options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 4;
});

// Add the IWebHostEnvironment service
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Setting Permanent Roles and Privileges in the system
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var privilegeService = scope.ServiceProvider.GetRequiredService<IPrivilegeService>();

    // Seed Roles
    var rolesToSeed = new string[] { "Admin", "Approver", "User Company", "User Individual" };
    foreach (var role in rolesToSeed)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new ApplicationRole(role));
        }
    }

    // Seed Privileges
    var privilegesToSeed = new string[] { "InitiateContract", "CreateUser", "DeleteUser" };
    foreach (var privilege in privilegesToSeed)
    {
        if (await privilegeService.GetPrivilegeByNameAsync(privilege) == null)
        {
            await privilegeService.CreatePrivilegeAsync(privilege);
        }
    }

    // Adding the admin user creation code
    string email = "admin@contractmanagement.com";
    string password = "Test1234!";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new AppUser { UserName = email, Email = email };
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
