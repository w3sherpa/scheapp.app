using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using scheapp.app.Data;
using scheapp.app.DataServices;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ScheApp") ?? throw new InvalidOperationException("Connection string 'scheappappContextConnection' not found.");

builder.Services.AddDbContext<ScheAppIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ScheAppIdentityContext>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;
builder.Services.AddHttpClient("ScheduleAppointmentApi", client =>
{
    string authUser = builder.Configuration["ScheduleAppointmentApi:BasicAuthUid"]!;
    string authPwd = builder.Configuration["ScheduleAppointmentApi:BasicAuthPwd"]!;
    string authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{authUser}:{authPwd}"));
    client.BaseAddress = new Uri(builder.Configuration["ScheduleAppointmentApi:BaseURL"]!);
    client.DefaultRequestHeaders.Authorization = null;
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
});

builder.Services.AddScoped<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IBusinessDataService, BusinessDataService>();
builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();
builder.Services.AddScoped<IProfessionalDataService, ProfessionalDataService>();
builder.Services.AddScoped<IServiceDataService, ServiceDataService>();
builder.Services.AddScoped<IContactsDataService, ContactsDataService>();
builder.Services.AddScoped<ICommunicationDataService, CommunicationDataService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
