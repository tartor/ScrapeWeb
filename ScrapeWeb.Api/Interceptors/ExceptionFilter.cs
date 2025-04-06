using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
	private readonly ILogger<GlobalExceptionFilter> _logger;

	public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
	{
		_logger = logger;
	}

	public override void OnException(ExceptionContext context)
	{
		_logger.LogError(context.Exception, "Unhandled Exception Occurred");

		var response = new
		{
			Message = "An error occurred while processing your request.",
			Error = context.Exception.Message
		};

		context.Result = new ObjectResult(response)
		{
			StatusCode = 500 // Internal Server Error
		};

		context.ExceptionHandled = true; // Mark exception as handled
	}
}