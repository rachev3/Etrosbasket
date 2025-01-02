using Etrosbasket.Areas.Admin.ViewModels.Articles;
using Etrosbasket.Models;
using Etrosbasket.Services.Implementations;
using Etrosbasket.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace Etrosbasket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("~/Areas/Admin/Views/Article/_CreateArticle.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubmit(ArticleCreateViewModel article)
        {
            await articleService.Add(article);
            return Json(new { success = true, message = "Player created successfully!" });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int articleId)
        {
            var viewModel = await articleService.GetById(articleId);
            return View("~/Areas/Admin/Views/Article/EditArticle.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditSave(ArticleEditViewModel viewModel)
        {
            await articleService.Update(viewModel);
            return RedirectToAction("Index");
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
     
    }
}
