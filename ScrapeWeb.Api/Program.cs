using Microsoft.AspNetCore.Diagnostics;
using ScrapeAppServer.Interface;
using ScrapeWeb.Api.Infrastructure;
using ScrapeWeb.Api.Interceptors;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Services;
using SessionAppServer.Interface;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IScrapeProxy, ScrapeProxy>();
builder.Services.AddScoped<ISessionProxy, SessionProxy>();
builder.Services.AddScoped<SessionHelper, SessionHelper>();

// Fetch service URLs from configuration
var serviceUrls = builder.Configuration.GetSection("ServiceUrls");

// Fetch from config or Consul
string scrapeServiceUrl = serviceUrls["ScrapeServiceUrl"];
//ScrapeAppServer WcfClient
using var wcfClientScrape = new WcfClient<IScrapeService>(scrapeServiceUrl);
builder.Services.AddScoped<IScrapeService>(sp =>
{
	return wcfClientScrape.Proxy();
});

// Fetch from config or Consul
string reportServiceUrl = serviceUrls["ReportServiceUrl"];
//ScrapeAppServer WcfClient
using var wcfClientReport = new WcfClient<IReportService>(reportServiceUrl);
builder.Services.AddScoped<IReportService>(sp =>
{
	return wcfClientReport.Proxy();
});

// Fetch from config or Consul
string sessionServiceUrl = serviceUrls["SessionServiceUrl"];
using var wcfClientSession = new WcfClient<ISessionService>(sessionServiceUrl);
builder.Services.AddScoped<ISessionService>(sp =>
{
	return wcfClientSession.Proxy();
});


builder.Services.AddScoped<GlobalExceptionFilter>(); 
// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
			   .AllowAnyMethod()
               .AllowAnyHeader()
			   .AllowCredentials();
    });
});

builder.Services.AddControllers(options =>
{
	options.Filters.Add<GlobalExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS middleware
app.UseCors("AllowLocalhost");

// Global exception handler middleware
app.UseMiddleware<GlobalExceptionMiddleware>(); 

app.MapControllers();

app.Run();
