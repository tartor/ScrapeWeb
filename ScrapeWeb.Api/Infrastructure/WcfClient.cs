using System;
using System.ServiceModel;



namespace ScrapeWeb.Api.Infrastructure;

public class WcfClient<T> : IDisposable where T : class
{
	private readonly ChannelFactory<T> _factory;

	public WcfClient(string serviceUrl)
	{
		var binding = new BasicHttpBinding();
		var address = new EndpointAddress(serviceUrl);

		_factory = new ChannelFactory<T>(binding, address);
	}

	public T Proxy() => _factory.CreateChannel();

	public void Dispose()
	{
		if (_factory != null)
		{
			try
			{
				_factory.Close(); // Graceful closure
			}
			catch
			{
				_factory.Abort(); // Fallback if closing fails
			}
		}
	}
}
