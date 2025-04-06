using ScrapeAppServer.Interface;

namespace ScrapeAppServer.Providers
{
	public interface IScrapeProvider
	{
		Task<Scrape> ScrapeAsync(int loginId, string url, string selector);
	}
}
