using CoreWCF;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using ScrapeAppServer.Interface;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

using ScrapeAppServer.Scrapers;
using System.Runtime.Serialization.DataContracts;
using SessionAppServer.Repositories;
using SessionAppServer.DB;
using ScrapeAppServer.Providers;

namespace ScrapeAppServer;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]

public class ScrapeService : IScrapeService
{
	IScrapeProvider _scrapeProvider;

	public ScrapeService(IScrapeProvider scrapeProvider)
	{
		_scrapeProvider = scrapeProvider;
	}

	public async Task<Scrape> ScrapeAsync(int loginId, string url, string selector)
	{
		return await _scrapeProvider.ScrapeAsync(loginId, url, selector);
	}
}
