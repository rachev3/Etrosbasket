using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
