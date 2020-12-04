using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FotografYukleme.Controllers
{
    public class YuklemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FotografYukleme")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
			long size = files.Sum(f => f.Length);

			var filePaths = new List<string>();		
			foreach (var formFile in files)
			{
				if (formFile.Length > 0)
				{
					var filePath = Path.GetTempFileName();
					filePaths.Add(filePath);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await formFile.CopyToAsync(stream);
					}
				}
			}

			return Ok(new { count = files.Count, size, filePaths });
		}
    }
}
