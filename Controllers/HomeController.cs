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
            var homeViewModel = await _service.GetHomeAsync();
            return View(homeViewModel);
        }

        public async Task<IActionResult> Entrepreneur(int id)
        {
            var entrepreneurViewModel = await _service.GetHomeEntrepreneurAsync(id);

            if (entrepreneurViewModel == null)
                return NotFound();

            return View(entrepreneurViewModel);
        }

        public async Task<IActionResult> Entrepreneurship(int id)
        {
            var entrepreneurshipViewModel = await _service.GetHomeEntrepreneurshipAsync(id);

            if (entrepreneurshipViewModel == null)
                return NotFound();

            return View(entrepreneurshipViewModel);
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
