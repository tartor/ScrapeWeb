using System.Runtime.Serialization;

namespace SessionAppServer.Interface;
[DataContract]
public class SessionResult
{
	[DataMember]
	public bool Valid { get; set; }
	[DataMember]
	public string Error { get; set; }
}
