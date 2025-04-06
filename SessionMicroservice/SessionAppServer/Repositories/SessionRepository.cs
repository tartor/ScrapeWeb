using Microsoft.EntityFrameworkCore;
using SessionAppServer.DB;
using System;

namespace SessionAppServer.Repositories;

public interface ISessionRepository
{
	Task<Login> LoginAsync(string username, string password);
	Task<Session> AddSessionAsync(string sessionId, int loginId);
	Task<Session> GetSessionAsync(string sessionId);
	Task<Session> CloseSessionAsync(string sessionId);
}

public class SessionRepository : ISessionRepository
{
	private readonly SessionDbContext _dbContext;

	public SessionRepository(SessionDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbContext.Database.EnsureCreated();
	}

	public async Task<Login> LoginAsync(string username, string password)
	{
		return await (
			from u in _dbContext.Logins
			where u.Username == username && u.Password == password
			select u
		).FirstOrDefaultAsync();
	}

	public async Task<Session> AddSessionAsync(string sessionId, int loginId)
	{
		var session = new Session
		{
			LoginId = loginId,
			SessionId = sessionId,
			Added = DateTime.Now,
			Expires = DateTime.Now.AddMinutes(60),
			Valid = true
		};
		await _dbContext.Sessions.AddAsync(session);
		await _dbContext.SaveChangesAsync();
		return await GetSessionAsync(sessionId);
	}

	public async Task<Session> GetSessionAsync(string sessionId)
	{
		return await (
			from u in _dbContext.Sessions
			where u.SessionId == sessionId
			select u
		).FirstOrDefaultAsync();
	}

	public async Task<Session> CloseSessionAsync(string sessionId)
	{
		var session = await GetSessionAsync(sessionId);
		if (session == null)
			return null;

		if(!session.Valid)
			return session;

		session.Valid = false;
		session.Ended = DateTime.Now;
		_dbContext.Sessions.Update(session);
		await _dbContext.SaveChangesAsync();
		
		return await GetSessionAsync(sessionId);

	}


}