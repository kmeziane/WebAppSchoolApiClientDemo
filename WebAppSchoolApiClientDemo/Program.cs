using EventSWebAppSchoolApiClientDemoharing.ConfigurationMapping;
using Microsoft.EntityFrameworkCore;
using WebAppSchoolApiClientDemo;
using WebAppSchoolApiClientDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SchoolDemoContext>(options =>
    options.UseSqlServer(connectionString));

var schoolApiUrl = builder.Configuration.GetValue<string>("ApiSettings:SchoolApiUrl");
builder.Services.AddScoped<SchoolApiClient>(c => new SchoolApiClient(schoolApiUrl, new HttpClient()));

builder.Services.AddAutoMapper(typeof(MappingProfile));
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
