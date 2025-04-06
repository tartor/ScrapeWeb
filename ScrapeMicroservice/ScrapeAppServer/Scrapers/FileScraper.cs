
namespace ScrapeAppServer.Scrapers
{
	public class FileScraper : IScraper
	{
		
		public async Task<string> GetContentAsync(string url)
		{
			Random random = new Random();
			int v = random.Next(1, 4);
			return await File.ReadAllTextAsync($"./Resources/google_{v}.html");
		}
	}
}
