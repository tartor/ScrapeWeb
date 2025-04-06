using ScrapeAppServer.Interface;
using ScrapeWeb.Api.Models;

namespace ScrapeWeb.Api.Interfaces;

public interface IScrapeProxy
{
	Task<List<Scrape>> ReportAsync(DateTime? from, DateTime? to);
	Task<Scrape> ScrapeAsync(string url, string selector);
}
