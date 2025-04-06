namespace ScrapeWeb.Api.Models;

public class ReportResponse
{
	public List<ScrapeItem> Items { get; set; }
	public string Error { get; set; }
}
