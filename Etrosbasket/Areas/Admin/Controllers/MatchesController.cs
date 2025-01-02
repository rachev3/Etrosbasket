using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
