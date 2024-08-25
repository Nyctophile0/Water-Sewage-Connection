using Microsoft.AspNetCore.Mvc;

namespace WaterSewageConnection.Controllers
{
	public class JEController : Controller
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
