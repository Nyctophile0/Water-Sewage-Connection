using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WaterSewageConnection.Controllers
{
	[Authorize(Roles = "AE")]
	public class AEController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Dashboard()
		{
			return View();
		}

		public IActionResult ProposedApplications()
		{
			return View();
		}
	}
}
