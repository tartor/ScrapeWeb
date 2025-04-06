using System.ComponentModel.DataAnnotations;

namespace SessionAppServer.DB;

public class Login
{
	[Key]
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }

}
