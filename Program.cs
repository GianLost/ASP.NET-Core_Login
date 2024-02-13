using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Services;
using ASP.NET_Core_Login.Helper.Messages;
using ASP.NET_Core_Login.Helper.Validation;
using ASP.NET_Core_Login.Helper.Authentication;
using ASP.NET_Core_Login.Helper.Authentication.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession(x =>
{
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

builder.Services.AddMvc();

builder.Services.AddDbContext<LoginContext>(options =>
{
    string? connectionString = Environment.GetEnvironmentVariable("SIS_LOGIN_CONNECTION_STRING") ?? throw new InvalidOperationException(FeedbackMessages.ErrorConnectionString);

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IUserValidation, UserValidation>();
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<ICryptography, Cryptography>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(name: "default", pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();