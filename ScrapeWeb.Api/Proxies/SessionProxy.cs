using ScrapeAppServer.Interface;
using ScrapeWeb.Api.Interfaces;
using SessionAppServer.Interface;

namespace ScrapeWeb.Api.Services;

public class SessionProxy : ISessionProxy
{
	ISessionService _sessionService;

	public SessionProxy(ISessionService sessionService)
	{
		_sessionService = sessionService;
	}

	public async Task<LoginResult> LoginAsync(string username, string password)
	{
		return await _sessionService.LoginAsync(username, password);
	}

	public async Task<SessionResult> ValidateAsync(string sessionId)
	{
		return await _sessionService.ValidateAsync(sessionId);
	}

	public async Task<LogoutResult> LogoutAsync(string sessionId)
	{
		return await _sessionService.LogoutAsync(sessionId);
	}

}
