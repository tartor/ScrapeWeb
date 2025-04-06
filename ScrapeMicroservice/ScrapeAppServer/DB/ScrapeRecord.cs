using System.ComponentModel.DataAnnotations;

namespace SessionAppServer.DB;

public class ScrapeRecord
{
	[Key]
	public int Id { get; set; }
	public int LoginId { get; set; }
	public int Place { get; set; }
	public DateTime Stamp { get; set; }
	public string Url { get; set; }
	public string Selector { get; set; }
}
