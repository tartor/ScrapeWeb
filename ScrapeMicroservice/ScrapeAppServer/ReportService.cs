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

namespace ScrapeAppServer;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]

public class ReportService : IReportService
{
	IReportRepository _repository;

	public ReportService(IReportRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Scrape>> ReportAsync(int loginId, DateTime? from, DateTime? to)
	{
		var records = await _repository.GetScrapesAsync(loginId, from, to);
		return records.Select( record =>
			new Scrape()
			{
				Place = record.Place,
				Stamp = record.Stamp,
				Url = record.Url,
				Selector = record.Selector
			}).ToList();
	}
}
