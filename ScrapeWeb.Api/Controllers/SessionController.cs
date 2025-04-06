using Microsoft.AspNetCore.Mvc;
using ScrapeWeb.Api.Infrastructure;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Models;
using ScrapeWeb.Api.Services;

namespace ScrapeWeb.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SessionController : ControllerBase
{
    private readonly ILogger<SessionController> _logger;
	private readonly ISessionProxy _sessionService;

	public SessionController(ISessionProxy sessionService, ILogger<SessionController> logger)
    {
		_sessionService = sessionService;
		_logger = logger;
    }

	[HttpPost]
	public async Task<SessionResponse> Validate([FromServices] SessionHelper sessionHelper)
	{
		var sessionId = sessionHelper.GetSessionId();
		var result = await _sessionService.ValidateAsync(sessionId);

		return new()
		{ 
			Valid = result.Valid, 
			Error = result.Error 
		};
	
	}

}
