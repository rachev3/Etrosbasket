using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return PartialView("~/Areas/Admin/Views/Article/_CreateArticle.cshtml");
        }
    }
}
