using PROJECTGREEN.Authentication;
using PROJECTGREEN.Models;
using Microsoft.AspNetCore.Identity;
using PROJECTGREEN.Manager;
using PROJECTGREEN.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.Configure<TokenAuthentication>(builder.Configuration.GetSection("TokenAuthentication"));
builder.Services.AddScoped<TokenValidationParametersFactory>();
builder.Services.AddScoped<SignInManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenProviderOptionsFactory>();
builder.Services.AddScoped<MailManager>();


builder.Services.AddScoped<BaseRepository<EmailsForNotificationAlert>>();
builder.Services.AddSingleton<IHostedService, EmailMonitoringBackgroundTask>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "projectgreen"; // Set the default authentication scheme
    options.DefaultSignInScheme = "projectgreen"; // Set the sign-in scheme
    options.DefaultChallengeScheme = "projectgreen"; // Set the challenge scheme
}).AddCookie("projectgreen", options =>
{
    options.LoginPath = "/Account/Login"; // Define where to redirect if not authenticated
    options.LogoutPath = "/Account/Logout"; // Define logout path
    options.ExpireTimeSpan = TimeSpan.FromDays(1); // Set cookie expiration
    options.SlidingExpiration = true; // Optional: enable sliding expiration
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Required for session to work
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Add authentication middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
