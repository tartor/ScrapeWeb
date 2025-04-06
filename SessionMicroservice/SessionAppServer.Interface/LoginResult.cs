using System.Runtime.Serialization;

namespace SessionAppServer.Interface;
[DataContract]
public class LoginResult
{
	[DataMember]
	public bool Success { get; set; }
	[DataMember]
	public string SessionId { get; set; }
	[DataMember]
	public string Error { get; set; }

}
