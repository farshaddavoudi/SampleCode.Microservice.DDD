using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Web.API.Extensions;
using SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// Configure Dependencies with Service Installers 
var appSettings = builder.Configuration.Get<AppSettings>();
appSettings!.IsDevelopment = builder.Environment.IsDevelopment();
builder.Services.ConfigureServicesWithInstallers(appSettings);

// Configure Logging
builder.Host.ConfigureSerilog(appSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Specialized middleware(s) for development
    app.UseMiniProfiler();
}

app.UseMiddleware<CorrelationIdHandlingMiddleware>();

// Serilog
app.UseMiddleware<AddCorrelationIdToSerilogContextMiddleware>();
app.UseMiddleware<AddClientIpToSerilogContextMiddleware>();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Audit.NET
app.UseMiddleware<AddCorrelationIdToAuditNetLogsMiddleware>();
app.UseMiddleware<AddClientIpToAuditNetLogsMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseRequestLocalization();

app.UseAuthentication();

app.UseMiddleware<ClaimsMiddleware>();

app.UseAuthorization();

app.UseMiddleware<AddCurrentUserToSerilogContextMiddleware>();

app.UseMiddleware<AddCurrentUserToAuditNetLogsMiddleware>();

app.ConfigureAuditNetLogging(appSettings);

app.ConfigureHangfireDashboard(appSettings);

app.MapControllers().RequireAuthorization();

app.Run();

