using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using scheapp.app.Data;
using scheapp.app.DataServices;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
// Add serilog
builder.Host.UseSerilog((context, configuration) => {
    configuration.Enrich.FromLogContext()
    //.WriteTo.Console()
    .WriteTo.Elasticsearch(
        new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"]))
        {
            //setting up warning level to elastic sink, file write and console can be set at different level
            TypeName = null,
            MinimumLogEventLevel = Serilog.Events.LogEventLevel.Warning,
            IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            BatchAction = ElasticOpType.Create,
            //EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
            //				   EmitEventFailureHandling.WriteToFailureSink |
            //				   EmitEventFailureHandling.RaiseCallback,
            //////Following setting are needed if the elastic search is configured for security with client login and sll. 
            ////// We are disabling that for faster communicationa and the hosting server will be internal not exposed
            //ModifyConnectionSettings = x => 
            //    x.BasicAuthentication(context.Configuration["ElasticSearch:User"], context.Configuration["ElasticSearch:Password"])
            //    .CertificateFingerprint("4E:45:89:B7:6D:A2:6B:74:E0:49:1C:98:5B:A5:E2:D2:8C:BA:BE:AE:D7:84:9E:2B:31:8F:9E:CD:64:AE:B0:27")
        });

});

var connectionString = builder.Configuration.GetConnectionString("ScheApp") ?? throw new InvalidOperationException("Connection string 'scheappappContextConnection' not found.");

builder.Services.AddDbContext<ScheAppIdentityContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ScheAppIdentityContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ScheAppIdentityContext>();

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
builder.Services.AddSignalR();
builder.Services.AddScoped<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IBusinessDataService, BusinessDataService>();
builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();
builder.Services.AddScoped<IProfessionalDataService, ProfessionalDataService>();
builder.Services.AddScoped<IServiceDataService, ServiceDataService>();
builder.Services.AddScoped<IContactsDataService, ContactsDataService>();
builder.Services.AddScoped<ICommunicationDataService, CommunicationDataService>();

var app = builder.Build();
app.Logger.LogWarning("ScheApp app started.");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ScheAppViewUpdateHub>("/ScheAppViewUpdateHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
