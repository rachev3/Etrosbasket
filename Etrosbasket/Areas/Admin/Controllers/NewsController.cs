using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
