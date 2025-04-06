namespace ScrapeAppServer.Scrapers
{
	public interface IScraper
	{
		Task<string> GetContentAsync(string url);
	}
}
