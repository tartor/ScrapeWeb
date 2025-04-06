using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.EntityFrameworkCore;
using SessionAppServer;
using SessionAppServer.DB;
using SessionAppServer.Interface;
using SessionAppServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
builder.Services.AddTransient<SessionService>();
builder.Services.AddTransient<ISessionRepository, SessionRepository>();

builder.Services.AddDbContext<SessionDbContext>(options =>
	options.UseInMemoryDatabase("SessionDB"));

var app = builder.Build();

// Configure an explicit none credential type for WSHttpBinding as it defaults to Windows which requires extra configuration in ASP.NET
var myWSHttpBinding = new WSHttpBinding(SecurityMode.Transport);
myWSHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

app.UseServiceModel(builder =>
{
	builder.AddService<SessionService>((serviceOptions) => { serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true; })
	// Add a BasicHttpBinding at a specific endpoint
	.AddServiceEndpoint<SessionService, ISessionService>(new BasicHttpBinding(), "/SessionService/basichttp")
	// Add a WSHttpBinding with Transport Security for TLS
	.AddServiceEndpoint<SessionService, ISessionService>(myWSHttpBinding, "/SessionService/WSHttps");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();
