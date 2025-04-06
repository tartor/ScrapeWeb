using System.Runtime.Serialization;

namespace SessionAppServer.Interface;
[DataContract]
public class LogoutResult
{
	[DataMember]
	public bool Success { get; set; }
	[DataMember]
	public string Error { get; set; }

}
