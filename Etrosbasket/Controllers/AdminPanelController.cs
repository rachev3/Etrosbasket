using Etrosbasket.Data.Services;
using Etrosbasket.Models;
using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Controllers
{
    public class AdminPanelController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
