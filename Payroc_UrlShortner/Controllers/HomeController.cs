using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Payroc_UrlShortner.Data;
using Payroc_UrlShortner.Models;
using System.Diagnostics;

namespace Payroc_UrlShortner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly URLDBContext _db;

        public HomeController(ILogger<HomeController> logger, URLDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Index_VM vm)
        {
            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            if (String.IsNullOrEmpty(vm.URL) == true)
            {
                ModelState.AddModelError("URL", "Please Enter a Valid URL.");
                return View(vm);
            }

            var url = new Url
            {
                URL = vm.URL
            };
            _db.Urls.Add(url);
            _db.SaveChanges();

            ViewData["Link"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{url.UrlChunk}";

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}