using System.Data;
using WaterSewageConnection.Models;

namespace WaterSewageConnection.Services
{
	public interface IConnectionService
	{
		Task<bool> AddConnection(ConnectionDetails obj);
		Task<string> UpdateConnection(ConnectionDetails obj);
		Task<DataSet> GetAllConnections(ConnectionDetails obj);
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
		
		public async Task<string> UpdateConnection(ConnectionDetails obj)
		{
			if (string.IsNullOrEmpty(obj.Action))
				obj.Action = "updateconnectiondetails";

			string message = await obj.saveAsync();

			return (!string.IsNullOrEmpty(message)) ? message : string.Empty;
		}

		public async Task<DataSet> GetAllConnections(ConnectionDetails obj)
		{
			// = new ConnectionDetails();
			if (string.IsNullOrEmpty(obj.Action))
				obj.Action = "selectallConnections";



			return await obj.getDataSetAsync();
		}
	}
}
