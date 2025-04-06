using ScrapeAppServer.Interface;
using ScrapeAppServer.Scrapers;
using SessionAppServer.Repositories;
using System.Text.RegularExpressions;

namespace ScrapeAppServer.Providers
{
	public class GoogleScrapeProvider : IScrapeProvider
	{
		const string _searchURL = "https://www.google.co.uk/search?num=100&q=";

		readonly string patternFormat = @"<a[^>]*href=""([^""]*{0}[^""]*)""[^>]*>";
		readonly string patternClass = @"<a[^>]*class=""(?<class>[^""]*)""[^>]*>";
		readonly string resultFormat = @"<a[^>]*class=""{0}""[^>]*>";

		IScraper _scraper;
		IScrapeRepository _repository;

		public GoogleScrapeProvider(IScrapeRepository repository, IScraper scraper)
		{
			_repository = repository;
			_scraper = scraper;
		}

		public async Task<Scrape> ScrapeAsync(int loginId, string url, string selector)
		{
			var fullUrl = $"{_searchURL}{Uri.EscapeDataString(selector)}";
			var content = await _scraper.GetContentAsync(fullUrl);

			//Find the class value of the result pattern
			var urlPattern = string.Format(patternFormat, Regex.Escape(url));
			var urlMatches = Regex.Matches(content, urlPattern, RegexOptions.IgnoreCase);
			var urlMatched = urlMatches.FirstOrDefault();
			if (urlMatched == null)
			{
				await _repository.AddScrapeAsync(new()
				{
					LoginId = loginId,
					Place = -1,
					Stamp = DateTime.Now,
					Url = url,
					Selector = selector
				});

				return new()
				{
					Place = -1,
					Stamp = DateTime.Now,
					Url = url,
					Selector = selector
				};
			}

			//extract the class of the urlMatched so we can select all the results
			var classMatches = Regex.Matches(urlMatched.Value, patternClass, RegexOptions.IgnoreCase);
			var classMatched = classMatches.FirstOrDefault();
			if (classMatched == null)
			{
				await _repository.AddScrapeAsync(new()
				{
					LoginId = loginId,
					Place = -1,
					Stamp = DateTime.Now,
					Url = url,
					Selector = selector
				});

				return new()
				{
					Place = -1,
					Stamp = DateTime.Now,
					Url = url,
					Selector = selector
				};
			}

			//use the matched class to select all the results
			var resultClass = classMatched.Groups["class"].Value;
			var resultPattern = string.Format(resultFormat, Regex.Escape(resultClass));
			var resultMatches = Regex.Matches(content, resultPattern, RegexOptions.IgnoreCase);
			int position = 0;
			foreach (Match match in resultMatches)
			{
				position++;
				urlMatches = Regex.Matches(match.Value, urlPattern, RegexOptions.IgnoreCase);
				if (urlMatches.Any())
				{
					break;
				}
			}

			await _repository.AddScrapeAsync(new()
			{
				LoginId = loginId,
				Place = position,
				Stamp = DateTime.Now,
				Url = url,
				Selector = selector
			});

			return new()
			{
				Place = position,
				Stamp = DateTime.Now,
				Url = url,
				Selector = selector
			};



		}
	}
}
