using Etrosbasket.Models.ViewModels;
using Etrosbasket.Models;
using Etrosbasket.Services.Interfaces;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace Etrosbasket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IPlayerStatisticService playerStatisticService;

        public PlayerController(IPlayerService playerService, IPlayerStatisticService playerStatisticService)
        {
            this.playerService = playerService;
            this.playerStatisticService = playerStatisticService;
        }
        //public async Task<IActionResult> Index(int playerId)
        //{
        //    var player = await playerService.GetById(playerId);
        //    PlayerPageViewModel viewModel = new(player);
        //    return View(viewModel);
        //}
        public async Task<IActionResult> Index()
        {
            var players = await playerService.GetAll();

            return View("Index", players);
        }
        public IActionResult Create()
        {

            return PartialView("~/Views/Player/_CreatePlayer.cshtml");
        }
        public async Task<IActionResult> CreateSubmit(Player player)
        {
            if (ModelState.IsValid)
            {
                await playerService.Add(player);

            }
            return RedirectToAction("Players");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int playerId)
        {
            var player = await playerService.GetById(playerId);
            return PartialView("~/Views/AdminPanel/_EditPlayer.cshtml", player);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Player player)
        {


            await playerService.Update(player.PlayerId, player);

            return PartialView("~/Views/AdminPanel/_EditPlayer.cshtml", player);
        }
        public IActionResult LoadUploadModalContent(int playerId, string playerName)
        {
            ViewBag.playerId = playerId;
            ViewBag.playerName = playerName;
            return PartialView("~/Views/AdminPanel/_UploadStatistic.cshtml");
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

        [HttpPost]
        public async Task<IActionResult> Delete(int playerId)
        {
            try
            {
                await playerService.Delete(playerId);
                return Json(new { success = true, message = "Player deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting player: {ex.Message}" });
            }
        }
        public async Task<IActionResult> UploadStatistic(IFormFile statisticPdf, int playerId, string playerName)
        {

            if (statisticPdf == null || statisticPdf.Length == 0)
            {
                return BadRequest("Invalid PDF file.");
            }

            using (var stream = new MemoryStream())
            {
                await statisticPdf.CopyToAsync(stream);
                stream.Position = 0;

                // Extract lines from PDF
                var lines = ExtractLinesFromPdf(stream);

                // Parse player statistics
                var playerStatistics = ParsePlayerStatistics(lines, playerId, playerName);
                if (playerStatistics != null)
                {
                    await playerStatisticService.Add(playerStatistics);
                    return Json(new { success = true, message = "Statistic uploaded successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "This statistic is already uploaded or the player has not played in this game." });
                }


                // Display or save player statistics
                //return View("StatisticsSummary", playerStatistics);
            }
        }

        public List<string> ExtractLinesFromPdf(Stream pdfStream)
        {
            var lines = new List<string>();

            using (var reader = new PdfReader(pdfStream))
            using (var pdfDoc = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    var strategy = new LocationTextExtractionStrategy();
                    var pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

                    // Split text into lines
                    lines.AddRange(pageText.Split('\n'));
                }
            }

            return lines;
        }

        public PlayerStatistic ParsePlayerStatistics(List<string> lines, int playerId, string playerName)
        {
            var playerStatistic = new PlayerStatistic();

            var teamAgainst = lines[5].Split('–')[0].Trim().Contains("Етрос")
                              ? string.Join(" ", lines[5].Split('–')[1].Trim().Split(' ').Where(word => !int.TryParse(word, out _)))
                              : string.Join(" ", lines[5].Split('–')[0].Trim().Split(' ').Where(word => !int.TryParse(word, out _)));


            playerStatistic.TeamAgainst = teamAgainst;

            var date = DateTime.ParseExact(lines[2].Split(',')[1].Trim().Split("Start time:")[0].Trim(), "ddd dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            playerStatistic.Date = date;

            playerStatistic.PlayerId = playerId;

            bool playerHasPlayed = false;
            foreach (var line in lines)
            {
                if (line.ToLower().Contains(playerName.ToLower()))
                {
                    var columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    playerStatistic.Minutes = TimeSpan.ParseExact(columns[3], "mm\\:ss", null);
                    playerStatistic.TwoPoints_Made = int.Parse(columns[6].Split('/')[0]);
                    playerStatistic.TwoPoints_Attempted = int.Parse(columns[6].Split('/')[1]);
                    playerStatistic.ThreePoints_Made = int.Parse(columns[8].Split('/')[0]);
                    playerStatistic.ThreePoints_Attempted = int.Parse(columns[8].Split('/')[1]);
                    playerStatistic.FreeThrows_Made = int.Parse(columns[10].Split('/')[0]);
                    playerStatistic.FreeThrows_Attempted = int.Parse(columns[10].Split('/')[1]);
                    playerStatistic.OffensiveRebounds = int.Parse(columns[12]);
                    playerStatistic.DeffensiveRebounds = int.Parse(columns[13]);
                    playerStatistic.Assists = int.Parse(columns[15]);
                    playerStatistic.Turnovers = int.Parse(columns[16]);
                    playerStatistic.Steals = int.Parse(columns[17]);
                    playerStatistic.Blocks = int.Parse(columns[18]);
                    playerStatistic.PersonalFaul = int.Parse(columns[19]);
                    playerStatistic.FaulDrawned = int.Parse(columns[20]);
                    playerStatistic.PlusMinus = int.Parse(columns[21]);
                    playerStatistic.Efficiency = int.Parse(columns[22]);
                    playerStatistic.Points = int.Parse(columns[23]);
                    playerHasPlayed = true;
                }



            }
            var statistics = playerStatisticService.GetByPlayerId(playerId).Result;
            if (statistics.Any(s => s.TeamAgainst == teamAgainst && s.Date == date) || !playerHasPlayed)
            {
                return null;
            }
            else
            {
                return playerStatistic;
            }
        }


    }
}
