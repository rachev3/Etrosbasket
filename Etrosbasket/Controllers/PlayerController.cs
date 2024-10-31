using Etrosbasket.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Players()
        {
            var players = await playerService.GetAll();

            return PartialView("~/Views/AdminPanel/_Players.cshtml", players);
        }
        public async Task<IActionResult> Details(int playerId)
        {
         
            var player = await playerService.GetById(playerId);

            if (player == null)
            {
                return NotFound();
            }

            return PartialView("~/Views/AdminPanel/_PlayerDetails.cshtml", player); 
        }

        public async Task<IActionResult> Delete(int playerId)
        {
            await playerService.Delete(playerId);
            return RedirectToAction("Players");
        }
    }
}
