using PROJECTGREEN.Authentication;
using PROJECTGREEN.Models;
using Microsoft.AspNetCore.Identity;
using PROJECTGREEN.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.Configure<TokenAuthentication>(builder.Configuration.GetSection("TokenAuthentication"));
builder.Services.AddScoped<TokenValidationParametersFactory>();
//builder.Services.AddScoped<SignInManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenProviderOptionsFactory>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "PROJECTGREEN"; // Set the default authentication scheme
    options.DefaultSignInScheme = "PROJECTGREEN"; // Set the sign-in scheme
    options.DefaultChallengeScheme = "PROJECTGREEN"; // Set the challenge scheme
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
