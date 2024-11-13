using Etrosbasket.Data.Services;
using Etrosbasket.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

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
        public IActionResult Create()
        {

            return PartialView("~/Views/AdminPanel/_CreatePlayer.cshtml");
        }
        public IActionResult  LoadUploadModalContent(int playerId, string playerName)
        {
            ViewBag.playerId = playerId;
            ViewBag.playerName = playerName;
            return PartialView("~/Views/AdminPanel/_UploadStatistic.cshtml");
        }

        public async Task<IActionResult> CreateSubmit(Player player)
        {
            if (ModelState.IsValid)
            {
                await playerService.Add(player);

            }
            return RedirectToAction("Players");
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
                var playerStatistics = ParsePlayerStatistics(lines,playerId,playerName);

                // Display or save player statistics
                return View("StatisticsSummary", playerStatistics);
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

               playerStatistic.TeamAgainst = lines[5].Contains("Етрос") ? lines[5].Replace("Етрос", "").Trim().Split(' ')[0] : lines[5].Split(' ')[0]; 
               playerStatistic.Date = DateTime.ParseExact(lines[2].Split(',')[1].Trim().Split("Start time:")[0].Trim(), "ddd dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);          // To do: extract the date
               playerStatistic.PlayerId = playerId;
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

                }



            }
            return playerStatistic;
        }


    }
}
