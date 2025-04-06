using System.ComponentModel.DataAnnotations;

namespace ScrapeWeb.Api.Models;

public class ScrapeRequest
{
	[Required(ErrorMessage = "Url is required.")]
	public string Url { get; set; }

	[Required(ErrorMessage = "Phrase is required.")]
	public string Selector { get; set; }
}
