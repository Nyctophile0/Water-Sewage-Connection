using Microsoft.AspNetCore.Mvc;

namespace WaterSewageConnection.Controllers
{
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
