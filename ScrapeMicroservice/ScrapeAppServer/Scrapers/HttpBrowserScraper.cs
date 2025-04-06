using System.IO.Compression;

namespace ScrapeAppServer.Scrapers
{
	public class HttpBrowserScraper : IScraper
	{
		public async Task<string> GetContentAsync(string url)
		{
			var handler = new HttpClientHandler()
			{
				AllowAutoRedirect = true
			};
			using var httpClient = new HttpClient(handler);
			using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

			request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");
			request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng");
			request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
			request.Headers.TryAddWithoutValidation("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
			request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

			using var response = await httpClient.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			await using var responseStream = await response.Content.ReadAsStreamAsync();
			await using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
			using var streamReader = new StreamReader(decompressedStream);
			string content = await streamReader.ReadToEndAsync();
			return content;
		}
	}
}
