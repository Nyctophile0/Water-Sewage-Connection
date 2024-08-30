using WaterSewageConnection.Models;

namespace WaterSewageConnection.Services
{
	public interface IUserService
	{
		Task<bool> UserAuthentication(Users obj);
		Task<Users> GetUserRole(Users obj);
	}

	public class UserService : IUserService
	{
		public readonly IConfiguration _config;

		public UserService(IConfiguration config)
		{
			_config = config;
		}

		public async Task<bool> UserAuthentication(Users obj)
		{
			obj.Action = "checkuser";

			if (!string.IsNullOrEmpty(obj.userName) && !string.IsNullOrEmpty(obj.password))
			{
				if (await obj.getUserDataAsync())
				{
					return true;
				}
			}

			return false;
		}

		public async Task<Users> GetUserRole(Users obj)
		{
			obj.Action = "getuserrole";
			if (!string.IsNullOrEmpty(obj.userName)  && !string.IsNullOrEmpty(obj.password))
			{
				if (await obj.getObjectAsync())
				{
					return obj;
				}
			}
			return obj;
		}
	}
}
