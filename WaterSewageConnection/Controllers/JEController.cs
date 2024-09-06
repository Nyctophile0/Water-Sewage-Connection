using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using WaterSewageConnection.Models;
using WaterSewageConnection.Services;

namespace WaterSewageConnection.Controllers
{
	[Authorize(Roles = "JE")]
	public class JEController : Controller
	{
		private readonly IConfiguration _config;
		private readonly IConnectionService _connectionService;
		private readonly IUserService _userService;

		public JEController(IConfiguration config, IConnectionService connectionService, IUserService userService)
		{
			_config = config;
			_connectionService = connectionService;
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Dashboard()
		{
			return View();
		}

		public async Task<IActionResult> ProposedApplications()
		{
			ConnectionDetails obj = new ConnectionDetails();

			// get current user role
			obj.Role = (HttpContext.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.User.Identity.Name)) ? HttpContext.User.Identity.Name.ToString() : null;

			var viewmodel = new ViewModel
			{
				ConnectionDataset = await _connectionService.GetAllConnections(obj),
				ConnectionModel = obj
			};

			return View(viewmodel);
		}
		
		public async Task<IActionResult> PendingChargesApplications()
		{
			ConnectionDetails obj = new ConnectionDetails();
			obj.Action = "selectpendingchargesapplications";
			// get current user role
			obj.Role = (HttpContext.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.User.Identity.Name)) ? HttpContext.User.Identity.Name.ToString() : null;

			var viewmodel = new ViewModel
			{
				ConnectionDataset = await _connectionService.GetAllConnections(obj),
				ConnectionModel = obj
			};

			return View(viewmodel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> JEPendingChargesUpdation(ViewModel viewmodel)
		{
			ConnectionDetails? obj = new ConnectionDetails();
			obj = viewmodel.ConnectionModel;
			obj.Action = "updatependingcharges";

			// get current user role
			obj.Role = (HttpContext.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.User.Identity.Name)) ?HttpContext.User.Identity.Name.ToString() : null;

			//if (HttpContext.User.Identity is ClaimsIdentity identity)
			//{
			//	var c = identity.FindFirst(ClaimTypes.Role).Value;
			//}

			if (string.IsNullOrEmpty(obj.Role.ToString()))
				return RedirectToAction("Error", "Account");

			// update connection details
			string msg = await _connectionService.UpdateConnection(obj);

			if (string.IsNullOrEmpty(msg))
				TempData["errormsg"] = "Error";
			TempData["errormsg"] = "Updated";

			return RedirectToAction("ProposedApplications", "JE");
		}
	}
}
