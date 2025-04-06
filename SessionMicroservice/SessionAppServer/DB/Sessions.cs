using System.ComponentModel.DataAnnotations;

namespace SessionAppServer.DB;

public class Session
{
	[Key]
	public int Id { get; set; }
	public string SessionId { get; set; }
	public DateTime Added { get; set; }
	public DateTime Expires { get; set; }
	public DateTime Ended { get; set; }
	public bool Valid { get; set; }
	public int LoginId { get; set; }
}
