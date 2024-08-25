using WaterSewageConnection.Models;

namespace WaterSewageConnection.Services
{
	public interface IUserService
	{
		Task<bool> UserAuthentication(Users obj);
	}

	public class UserService : IUserService
	{
		public readonly IConfiguration _config;

		public UserService(IConfiguration config)
		{
			config = _config;
		}

		public async Task<bool> UserAuthentication(Users obj)
		{
			return true;
		}
	}
}
