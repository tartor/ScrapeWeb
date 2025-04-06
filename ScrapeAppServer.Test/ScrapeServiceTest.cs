using Moq;
using ScrapeAppServer.Interface;
using ScrapeAppServer.Providers;
using ScrapeAppServer.Scrapers;
using SessionAppServer.Repositories;
using System.Text.RegularExpressions;

namespace ScrapeAppServer.Test
{
	[TestClass]
	public sealed class ScrapeServiceTest
	{
		private Mock<IScrapeRepository> _mockScrapeRepository;
		private Mock<IScraper> _mockScraper;
		private IScrapeProvider _scrapeProvider;
		private ScrapeService _scrapeService;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockScrapeRepository = new Mock<IScrapeRepository>();
			_mockScraper = new Mock<IScraper>();
			_mockScraper.Setup(x => x.GetContentAsync(It.IsAny<string>()))
				.Returns(async (string s) =>
				{
					return await File.ReadAllTextAsync("./Resources/google_1.html");
				});

			_scrapeProvider = new GoogleScrapeProvider(_mockScrapeRepository.Object, _mockScraper.Object);
			_scrapeService = new ScrapeService(_scrapeProvider);
		}

		[TestMethod]
		public async Task TestPatternAsync()
		{
			var scrape = await _scrapeService.ScrapeAsync(1, "www.infotrack.co.uk", "land registry searches");
			Assert.IsNotNull(scrape);
			Assert.IsTrue(scrape.Place == 19);
		}
	}
}
