using Microsoft.AspNetCore.Mvc;
using ScrapeWeb.Api.Interfaces;
using ScrapeWeb.Api.Models;
using ScrapeWeb.Api.Services;
using System.IO.Compression;
using System.Security.Authentication;
using System;
using ScrapeWeb.Api.Interceptors;
using ScrapeWeb.Api.Infrastructure;

namespace ScrapeWeb.Api.Controllers;

[TypeFilter(typeof(AuthenticatedAttribute))]
[ApiController]
[Route("[controller]/[action]")]
public class ScrapeController : ControllerBase
{
    private readonly ILogger<ScrapeController> _logger;
	private readonly IScrapeProxy _scrapeService;

	public ScrapeController(IScrapeProxy scrapeService, ILogger<ScrapeController> logger)
    {
		_scrapeService = scrapeService;
		_logger = logger;
    }

    [HttpPost]
    public async Task<ScrapeResponse> Scrape(ScrapeRequest request)
    {
		var scrape = await _scrapeService.ScrapeAsync(request.Url, request.Selector);
		return new() 
		{
			Item = new(){
				Place = scrape.Place,
				Stamp = scrape.Stamp,
				Selector = scrape.Selector,
				Url = scrape.Url
			}
		};
	}

}
