using System.ServiceModel;

namespace SessionAppServer.Interface;

[ServiceContract]
public interface ISessionService
{
	[OperationContract]
	Task<LoginResult> LoginAsync(string username, string password);
	[OperationContract]
	Task<SessionResult> ValidateAsync(string sessionId);
	[OperationContract]
	Task<LogoutResult> LogoutAsync(string sessionId);
	[OperationContract]
	Task<int> GetLoginId(string sessionId);

}
