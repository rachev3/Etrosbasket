using Etrosbasket.Services.Implementations;
using Etrosbasket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace Etrosbasket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public async Task<IActionResult> Index()
        {
            var players = await articleService.GetAll();

            return View("Index", players);
        }
        [HttpGet]
        public async Task<IActionResult> GetArticlesTable()
        {
            var players = await articleService.GetAll();
            return PartialView("_ArticlesTable", players);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int playerId)
        {
            try
            {
                await articleService.Delete(playerId);
                return Json(new { success = true, message = "Player deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting player: {ex.Message}" });
            }
        }
        public IActionResult Create()
        {
            return PartialView("~/Areas/Admin/Views/Article/_CreateArticle.cshtml");
        }
    }
}
