using SessionAppServer.Interface;

namespace ScrapeWeb.Api.Interfaces
{
	public interface ISessionProxy
	{
		Task<LoginResult> LoginAsync(string username, string password);
		Task<SessionResult> ValidateAsync(string sessionId);
		Task<LogoutResult> LogoutAsync(string sessionId);
	}

}
