using ScrapeAppServer.Interface;
using ScrapeWeb.Api.Infrastructure;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Models;
using SessionAppServer.Interface;

namespace ScrapeWeb.Api.Services;

public class ScrapeProxy : IScrapeProxy
{
	IScrapeService _scrapeService;
	IReportService _reportService;
	ISessionService _sessionService;
	SessionHelper _sessionHelper;

	public ScrapeProxy(IScrapeService scrapeService, IReportService reportService, ISessionService sessionService, SessionHelper sessionHelper)
	{
		_scrapeService = scrapeService;
		_reportService = reportService;
		_sessionService = sessionService;
		_sessionHelper = sessionHelper;
	}

	public async Task<Scrape> ScrapeAsync(string url, string selector)
	{
		int loginId = await _sessionService.GetLoginId(_sessionHelper.GetSessionId());
		return await _scrapeService.ScrapeAsync(loginId, url, selector);
	}

	public async Task<List<Scrape>> ReportAsync(DateTime? from, DateTime? to)
	{
		int loginId = await _sessionService.GetLoginId(_sessionHelper.GetSessionId());
		return await _reportService.ReportAsync(loginId, from, to);
	}

}
