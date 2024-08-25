using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WaterSewageConnection.Models;
using WaterSewageConnection.Services;

namespace WaterSewageConnection.Controllers
{
    public class AccountController : Controller
    {
		private readonly IConfiguration _config;
		private readonly ITokenService _tokenService;
		private readonly IUserService _userService;
		private readonly IUserService _userService1;

		public AccountController(IConfiguration config, ITokenService tokenService, IUserService userService)
		{
			_config = config;
			_tokenService = tokenService;
			_userService = userService;
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users obj)
        {
			obj.Role = "Owner";
			//obj.Role = "Admin";
			// define claims
			var claims = new[] { new Claim(ClaimTypes.Role, obj.Role), new Claim(ClaimTypes.Name, obj.userName) };

			var check_user = await _userService.UserAuthentication(obj);

			// get jwt token
			var accesstoken = _tokenService.GenerateJSONWebToken(claims);

			// save to cookie
			if (accesstoken != null)
				SetJWTCookie(accesstoken);
			else
				return Unauthorized();

			return RedirectToAction("Dashboard", "Owner");

		}

		public IActionResult Logout()
		{
			Response.Cookies.Delete("jwtToken");
			return RedirectToAction("Login");
		}

		private void SetJWTCookie(string token)
		{
			var cookie_options = new CookieOptions
			{
				HttpOnly = true,
				Secure = true, // use true in production
				SameSite = SameSiteMode.Strict // adjust according to needs
			};

			Response.Cookies.Append("jwtToken", token, cookie_options);
		}

		[HttpGet]
		public IActionResult OwnerRegistration()
		{
			return View();
		}

		[HttpPost]
		public IActionResult OwnerRegistration(OwnerDetails obj)
		{
            string msg = "";
            obj.Action = "insertowner";
            obj.save(out msg);

            return RedirectToAction("OwnerRegistration", "Account");
			//return View();
		}

	}
}
