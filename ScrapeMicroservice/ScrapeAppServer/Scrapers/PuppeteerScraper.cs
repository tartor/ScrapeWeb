
using PuppeteerSharp;

namespace ScrapeAppServer.Scrapers
{
	public class PuppeteerScraper : IScraper
	{
		public async Task<string> GetContentAsync(string url)
		{
			// Download Chromium if not already done
			var browserFetcher = new BrowserFetcher();
			await browserFetcher.DownloadAsync(); 

			using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = false // set to false if you want to see the browser
			});

			// Open a new page
			using var page = await browser.NewPageAsync();

			// Go to the target URL
			await page.GoToAsync(url);

			// Get full HTML content of the page
			string content = await page.GetContentAsync();

			return content;
		}
	}
}
