using Etrosbasket.Models;
using Etrosbasket.Services.Implementations;
using Etrosbasket.Services.Interfaces;
using Etrosbasket.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Etrosbasket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly IPlayerService _playerService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, IPlayerService playerService)
        {
            _logger = logger;
            _articleService = articleService;
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetTopTwoArticlesForHome();
            var players = await _playerService.GetStartingFive();

            HomePageViewModel viewModel = new()
            {
                Articles = articles,
                Players = players
            };
            return View(viewModel);
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
