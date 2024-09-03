using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WaterSewageConnection.Models;
using WaterSewageConnection.Services;

namespace WaterSewageConnection.Controllers
{
	[Authorize(Roles = "JE")]
	public class JEController : Controller
	{
		private readonly IConfiguration _config;
		private readonly IConnectionService _connectionService;

		public JEController(IConfiguration config, IConnectionService connectionService)
		{
			_config = config;
			_connectionService = connectionService;
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

			var viewmodel = new ViewModel
			{
				ConnectionDataset = await _connectionService.GetAllConnections(),
				ConnectionModel = obj
			};

			return View(viewmodel);
		}
	}
}
