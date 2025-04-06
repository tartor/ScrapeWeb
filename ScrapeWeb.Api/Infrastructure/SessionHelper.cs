namespace ScrapeWeb.Api.Infrastructure;

public class SessionHelper
{
	IHttpContextAccessor _httpContextAccessor;
	private const string SessionCookieName = "SCWAUTH";

	public SessionHelper(IHttpContextAccessor httpContextAccessor) {
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetSessionId()
	{
		var context = _httpContextAccessor.HttpContext;
		if (context == null)
		{
			throw new InvalidOperationException("HTTP context is not available.");
		}
		var sessionId = context.Request.Cookies[SessionCookieName];
		return sessionId;
	}

	public void SetSessionId(string sessionId)
	{
		var context = _httpContextAccessor.HttpContext;
		if (context == null)
		{
			throw new InvalidOperationException("HTTP context is not available.");
		}
		context.Response.Cookies.Append(SessionCookieName, sessionId, new CookieOptions
		{
			SameSite = SameSiteMode.None, // Allows cookies in top-level navigations and GETs
			Secure = true,              // Allow over HTTP in dev
			HttpOnly = true,
			Path = "/"
		});
	}

	public void ClearSessionId()
	{
		var context = _httpContextAccessor.HttpContext;
		if (context == null)
		{
			throw new InvalidOperationException("HTTP context is not available.");
		}
		context.Response.Cookies.Delete(SessionCookieName);
	}
}
