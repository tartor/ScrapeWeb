using System.ServiceModel;

namespace ScrapeAppServer.Interface;

[ServiceContract]
public interface IScrapeService
{
	[OperationContract]
	Task<Scrape> ScrapeAsync(int loginId, string url, string selector);
}

