using WaterSewageConnection.Models;

namespace WaterSewageConnection.Services
{
	public interface IConnectionService
	{
		Task<bool> AddConnection(ConnectionDetails obj);
	}

	public class ConnectionService : IConnectionService
	{
		public readonly IConfiguration _config;

		public ConnectionService(IConfiguration config)
		{
			_config = config;
		}

		public async Task<bool> AddConnection(ConnectionDetails obj)
		{
			obj.Action = "insertconnection";

			var message = await obj.saveAsync();

			if (!string.IsNullOrEmpty(message))
				return true;

			return false;
		}
	}
}
