using Microsoft.AspNetCore.Mvc;
using ScrapeWeb.Api.Infrastructure;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Models;
using ScrapeWeb.Api.Services;
using System.IO.Compression;
using System.Security.Authentication;

namespace ScrapeWeb.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
	private readonly ISessionProxy _sessionService;

	public LoginController(ISessionProxy sessionService, ILogger<LoginController> logger)
    {
		_sessionService = sessionService;
		_logger = logger;
    }

	[HttpPost]
	public async Task<LoginResponse> Login(LoginRequest request, [FromServices] SessionHelper sessionHelper)
	{
		var result = await _sessionService.LoginAsync(request.Username, request.Password);

		if (result.Success)
		{
			sessionHelper.SetSessionId(result.SessionId);
		}
		return new()
		{ 
			Success = result.Success, 
			Error = result.Error 
		};
	
	}

	[HttpPost]
	public async Task Logout([FromServices] SessionHelper sessionHelper)
	{
		var sessionId = sessionHelper.GetSessionId();
		await _sessionService.LogoutAsync(sessionId);
		sessionHelper.ClearSessionId();
	}
}
