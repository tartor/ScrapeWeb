using SessionAppServer.DB;
using SessionAppServer.Interface;
using SessionAppServer.Repositories;

namespace SessionAppServer;

public class SessionService : ISessionService
{
	ISessionRepository _repository;
	public SessionService(ISessionRepository repository)
	{
		_repository = repository;
	}

	public async Task<LoginResult> LoginAsync(string username, string password)
	{
		var login = await _repository.LoginAsync(username, password);
		if(login == null)
		{
			return new()
			{
				Success = false,
				Error = "Invalid login"
			};
		}
		var sessionId = Guid.NewGuid().ToString();
		await _repository.AddSessionAsync(sessionId, login.Id);

		return new()
		{
			Success = true,
			SessionId = sessionId,
			Error = null
		};
	}

	public async Task<SessionResult> ValidateAsync(string sessionId)
	{
		var session = await _repository.GetSessionAsync(sessionId);
		if (session == null)
		{
			return new()
			{
				Valid = false,
				Error = "Session not found"
			};
		}
		if (!session.Valid)
		{
			return new()
			{
				Valid = false,
				Error = "Invalid session"
			};
		}

		if (session.Expires <= DateTime.Now)
		{
			return new()
			{
				Valid = false,
				Error = "Invalid expired"
			};
		}

		return new()
		{
			Valid = true,
			Error = null
		};

	}

	public async Task<LogoutResult> LogoutAsync(string sessionId)
	{
		var session = await _repository.CloseSessionAsync(sessionId);
		if (session == null || session.Valid)
		{
			return new()
			{
				Success = false,
				Error = null
			};
		}

		return new()
		{
			Success = true,
			Error = null
		};
	}

	public async Task<int> GetLoginId(string sessionId)
	{
		var session = await _repository.GetSessionAsync(sessionId);
		return session?.LoginId ?? -1;
	}
}
