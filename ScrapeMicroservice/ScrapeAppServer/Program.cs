using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.EntityFrameworkCore;
using ScrapeAppServer;
using ScrapeAppServer.DB;
using ScrapeAppServer.Interface;
using ScrapeAppServer.Providers;
using ScrapeAppServer.Scrapers;
using SessionAppServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
builder.Services.AddTransient<ScrapeService>();
builder.Services.AddTransient<ReportService>();
builder.Services.AddTransient<IScrapeRepository, ScrapeRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();

builder.Services.AddTransient<IScrapeProvider,GoogleScrapeProvider>();
//builder.Services.AddTransient<IScraper, HttpScraper>();
//builder.Services.AddTransient<IScraper, HttpBrowserScraper>();
//builder.Services.AddTransient<IScraper, PuppeteerScraper>();
builder.Services.AddTransient<IScraper, FileScraper>();


builder.Services.AddDbContext<ScrapeDbContext>(options =>
	options.UseInMemoryDatabase("ScrapeDB"));

var app = builder.Build();

// Configure an explicit none credential type for WSHttpBinding as it defaults to Windows which requires extra configuration in ASP.NET
var myWSHttpBinding = new WSHttpBinding(SecurityMode.Transport);
myWSHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

app.UseServiceModel(builder =>
{
	builder.AddService<ScrapeService>((serviceOptions) => { serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true; })
	// Add a BasicHttpBinding at a specific endpoint
	.AddServiceEndpoint<ScrapeService, IScrapeService>(new BasicHttpBinding(), "/ScrapeService/basichttp")
	// Add a WSHttpBinding with Transport Security for TLS
	.AddServiceEndpoint<ScrapeService, IScrapeService>(myWSHttpBinding, "/ScrapeService/WSHttps");

});


app.UseServiceModel(builder =>
{
	builder.AddService<ReportService>((serviceOptions) => { serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true; })
	// Add a BasicHttpBinding at a specific endpoint
	.AddServiceEndpoint<ReportService, IReportService>(new BasicHttpBinding(), "/ReportService/basichttp")
	// Add a WSHttpBinding with Transport Security for TLS
	.AddServiceEndpoint<ReportService, IReportService>(myWSHttpBinding, "/ReportService/WSHttps");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();
