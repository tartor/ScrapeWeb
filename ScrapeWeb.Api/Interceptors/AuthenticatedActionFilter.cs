using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScrapeWeb.Api.Infrastructure;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Services;
using System.Diagnostics;

namespace ScrapeWeb.Api.Interceptors;

public class AuthenticatedAttribute : ActionFilterAttribute
{
	private SessionHelper _sessionHelper;
	private ISessionProxy _sessionService;
	public AuthenticatedAttribute(ISessionProxy sessionService, SessionHelper sessionHelper)
	{
		_sessionService = sessionService;
		_sessionHelper = sessionHelper;
	}

	/// <inheritdoc />
	public override async Task OnActionExecutionAsync(
		ActionExecutingContext context,
		ActionExecutionDelegate next)
	{

		var sessionId = _sessionHelper.GetSessionId();
		var result = await _sessionService.ValidateAsync(sessionId);
		if (!result.Valid)
		{
			context.Result = new UnauthorizedResult();
			return;
		}

		await base.OnActionExecutionAsync(context, next);
	}
	
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		base.OnActionExecuting(context);
	}

	public override void OnActionExecuted(ActionExecutedContext context)
	{
		base.OnActionExecuted(context);
	}
}