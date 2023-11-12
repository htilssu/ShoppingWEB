using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ShoppingWEB;
using ShoppingWEB.Models;

var conf = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


var builder = WebApplication.CreateBuilder(args);
var connectionString = conf["Database:ConnectionString"];

//Regist Service
builder.Services.AddSingleton(new MySqlConnection(connectionString));
builder.Services.AddDbContext<ShoppingContext>(optionsBuilder =>
{
    optionsBuilder.UseLazyLoadingProxies();
    optionsBuilder.UseMySQL(connectionString!);
    optionsBuilder.EnableSensitiveDataLogging(false);
});
builder.Services.AddDefaultIdentity<UserModel>().AddRoles<RoleModel>().AddEntityFrameworkStores<ShoppingContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(20);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.LoginPath = "/Login";
    options.LogoutPath = "/";
    options.Cookie.HttpOnly = true;
    options.Cookie.MaxAge = TimeSpan.FromHours(6);
    options.Cookie.Name = "Shopping_Cookie";
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Error");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "Area",
    "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}"
);


app.MapRazorPages();


app.Run();