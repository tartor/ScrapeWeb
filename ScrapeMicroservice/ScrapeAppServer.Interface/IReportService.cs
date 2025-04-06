using System.ServiceModel;

namespace ScrapeAppServer.Interface;

[ServiceContract]
public interface IReportService
{
	[OperationContract]
	Task<List<Scrape>> ReportAsync(int loginId, DateTime? from, DateTime? to);
}

