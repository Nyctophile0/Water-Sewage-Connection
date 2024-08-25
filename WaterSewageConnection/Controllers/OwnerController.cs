using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using WaterSewageConnection.Models;
using WaterSewageConnection.Services;

namespace WaterSewageConnection.Controllers
{
	//[Authorize]
	[Authorize(Roles ="Owner")]
	public class OwnerController : Controller
	{
		private readonly IConfiguration _config;
		private readonly ITokenService _tokenService;

		public OwnerController(IConfiguration config, ITokenService tokenService)
		{
			_config = config;
			_tokenService = tokenService;
		}

		public IActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> Dashboard()
		{
			//var jwt = Request.Cookies["jwtCookie"];
			//string token = HttpContext.Session.GetString("Token");
			//if (token == null)
			//{
			//	return RedirectToAction("Login", "Account");
			//}

			//var httpClient = new HttpClient();
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

			//if (!_tokenService.ValidateToken(token))
			//{
			//	return RedirectToAction("Login", "Account");
			//}

			return View();
		}


		public IActionResult Connection()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Connection(ConnectionDetails obj)
		{
			obj.guid = Guid.NewGuid().ToString();
			string msg = "";
			List<string> filetypes = new List<string>();
			List<IFormFile> files = new List<IFormFile>();
			obj.Action = "insertConnection";

			if (obj.HouseMap != null)
			{
				filetypes.Add("HouseMap");
				files.Add(obj.HouseMap);
			}
			if (obj.ElectricityBill != null)
			{
				filetypes.Add("ElectricityBill");
				files.Add(obj.ElectricityBill);
			}
			if (obj.HouseTax != null)
			{
				filetypes.Add("HouseTax");
				files.Add(obj.HouseTax);
			}
			if (obj.Registry != null)
			{
				filetypes.Add("Registry");
				files.Add(obj.Registry);
			}

			

			try
			{
				List<ConnectionDetails> uploadedFiles = await MultipleUploadFilesAsync(filetypes, files);

				foreach (var uploadedFile in uploadedFiles)
				{
					if (uploadedFile != null && uploadedFile.filepath != "nofileuploaded")
					{
						if (uploadedFile.filetype == "HouseMap")
						{
							obj.HouseMapPath = uploadedFile.filepath;
						}
						else if (uploadedFile.filetype == "ElectricityBill")
						{
							obj.ElectricityBillPath = uploadedFile.filepath;
						}
						else if (uploadedFile.filetype == "HouseTax")
						{
							obj.HouseTaxPath = uploadedFile.filepath;
						}
						else if (uploadedFile.filetype == "Registry")
						{
							obj.RegistryPath = uploadedFile.filepath;
						}
					}
				}

				obj.save(out msg);
				if (msg != "inserted")
					msg = "error";

				TempData["msg"] = msg;

				return RedirectToAction("Connection");
			}
			catch (Exception ex)
			{
				// Log the exception
				TempData["errormsg"] = ex.Message;
				return View(obj); 
			}
		}


		public async Task<List<ConnectionDetails>> MultipleUploadFilesAsync(List<string> filetypes, List<IFormFile> files)
		{
			List<ConnectionDetails> uploadedFiles = new List<ConnectionDetails>();

			try
			{
				for (int i = 0; i < files.Count; i++)
				{
					IFormFile file = files[i];

					if (file != null && file.Length > 0)
					{
						string fname = file.FileName;
						string extension = Path.GetExtension(fname);
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
						fname = fileNameWithoutExtension + extension;

						// Adjust file name as needed
						string nameOfFile = fname.Replace("~", "-");

						nameOfFile = filetypes[i] + "_" + nameOfFile;
						string signedPath = "~/Uploads/Files/" + nameOfFile;
						

						// Save file asynchronously using IFormFile's OpenReadStream and a FileStream
						using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Files/", nameOfFile), FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						ConnectionDetails obj = new ConnectionDetails();
						obj.filepath = signedPath;
						obj.filetype = filetypes[i];

						uploadedFiles.Add(obj);
					}
				}

				return uploadedFiles;
			}
			catch (Exception ex)
			{
				// Log the exception
				TempData["errormsg"]= ex.Message;
				throw; // Rethrow the exception to propagate it up the call stack
			}
		}


	}
}
