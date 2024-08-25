using Azure;using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WaterSewageConnection.Services
{
	public interface ITokenService
	{
		string GenerateJSONWebToken(Claim[] claims);

		bool ValidateToken(string token);
	}

	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;

		public TokenService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateJSONWebToken(Claim[] claims)
		{

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				//issuer: "WaterConnection",
				//audience: "dotnetclient",
				expires: DateTime.Now.AddMinutes(1),
				signingCredentials: credentials,
				claims: claims
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}



		public bool ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
			var parameters = new TokenValidationParameters
			{
				//ValidateIssuer = true,
				//ValidateAudience = true,
				//ValidIssuer = "WaterConnection",
				//ValidAudience = "dotnetclient",
				IssuerSigningKey = new SymmetricSecurityKey(key)
			};

			try
			{
				tokenHandler.ValidateToken(token, parameters, out _);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
