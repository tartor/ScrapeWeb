using System.Runtime.Serialization;

namespace ScrapeAppServer.Interface;

[DataContract]
public class Scrape
{
	[DataMember] 
	public DateTime Stamp { get; set; }

	[DataMember]
	public int Place { get; set; }

	[DataMember]
	public string Url { get; set; }
	[DataMember]
	public string Selector { get; set; }

}

