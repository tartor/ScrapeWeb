using System.Security.Authentication;

namespace ScrapeAppServer.Scrapers
{
	public class HttpScraper : IScraper
	{
		public async Task<string> GetContentAsync(string url)
		{
			var handler = new HttpClientHandler()
			{
				AllowAutoRedirect = true
			};
			using var httpClient = new HttpClient(handler);
			var response = await httpClient.GetAsync(url);
			return await response.Content.ReadAsStringAsync();
			
		}
	}
}
