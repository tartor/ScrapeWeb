namespace ScrapeWeb.Api.Models
{
	public class ScrapeItem
	{
		public DateTime Stamp { get; set; }
		public int Place { get; set; }

		public string Selector { get; set; }
		public string Url { get; set; }
	}
}
