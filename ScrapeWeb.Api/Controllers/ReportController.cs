using Microsoft.AspNetCore.Mvc;
using ScrapeWeb.Api.Interceptors;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Models;
using ScrapeWeb.Api.Services;

namespace ScrapeWeb.Api.Controllers;

[TypeFilter(typeof(AuthenticatedAttribute))]
[ApiController]
[Route("[controller]/[action]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
	private readonly IScrapeProxy _scrapeService;

	public ReportController(IScrapeProxy scrapeService, ILogger<ReportController> logger)
    {
		_scrapeService = scrapeService;
		_logger = logger;
    }

	[HttpPost]
	public async Task<ReportResponse> Report(ReportRequest request)
	{
		var scrapes = await _scrapeService.ReportAsync(request.From, request.To);
		return new()
		{
			Items = scrapes.Select
			(
				s => new ScrapeItem()	
				{
					Place = s.Place,
					Stamp = s.Stamp,
					Selector = s.Selector,
					Url = s.Url
				}
			).ToList()
		};
	}

}
