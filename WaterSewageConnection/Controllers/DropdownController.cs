using Microsoft.AspNetCore.Mvc;
using WaterSewageConnection.Models;

namespace WaterSewageConnection.Controllers
{
	public class DropdownController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public JsonResult fillDropdown(BindDropdown obj)
		{
			var data = obj.getDataToBind();
			return Json(data);
			//return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
		}
	}
}
