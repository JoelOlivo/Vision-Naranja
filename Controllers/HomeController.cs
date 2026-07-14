using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisionNaranja.Models;
using VisionNaranja.Services;

namespace VisionNaranja.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _service;

        public HomeController(HomeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = await _service.GetAsync();
            return View(homeViewModel);
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
