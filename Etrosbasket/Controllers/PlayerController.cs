using Etrosbasket.Data.Services;
using Etrosbasket.Models;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
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
        public IActionResult LoadUploadModalContent()
        {
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
        public async Task<IActionResult> UploadStatistic(IFormFile statisticPdf)
        {
            if (statisticPdf == null || statisticPdf.Length == 0)
            {
                return BadRequest("Invalid PDF file.");
            }

            using (var stream = new MemoryStream())
            {
                statisticPdf.CopyTo(stream);
                stream.Position = 0;

                // Extract text from PDF
                string pdfText = ExtractTextFromPdf(stream);

                // Parse player statistics
                var playerStatistics = ParsePlayerStatisticsPdf(pdfText);

                // You can now save each PlayerStatistic to the database, or display them
                return View("StatisticsSummary", playerStatistics);
            }
        }

        public string ExtractTextFromPdf(Stream pdfStream)
        {
            using (PdfReader reader = new PdfReader(pdfStream))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }
        public List<PlayerStatistic> ParsePlayerStatisticsPdf(string pdfText)
        {
            var playerStatistics = new List<PlayerStatistic>();

            // Regex pattern to match each player's statistics line
            var playerPattern = new Regex(@"(\d+)\s+([^\d]+?)\s+(\d{1,2}:\d{2})\s+(\d+/\d+)\s+([\d,]+)\s+(\d+/\d+)\s+([\d,]+)\s+(\d+/\d+)\s+([\d,]+)\s+(\d+/\d+)\s+([\d,]+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(-?\d+)\s+(\d+)\s+(\d+)");

            foreach (Match match in playerPattern.Matches(pdfText))
            {
                var statistic = new PlayerStatistic();

                // Parsing fields from regex groups, ensure each group matches model property
                statistic.TeamAgainst = "Opponent Team Name"; // Set this based on context if available
                statistic.Date = DateTime.Parse("2024-10-05"); // Set the game date
                statistic.Minutes = TimeSpan.Parse(match.Groups[2].Value);

                // Field Goals
                statistic.TwoPoints_Made = int.Parse(match.Groups[5].Value.Split('/')[0]);
                statistic.TwoPoints_Attempted = int.Parse(match.Groups[5].Value.Split('/')[1]);
                statistic.ThreePoints_Made = int.Parse(match.Groups[7].Value.Split('/')[0]);
                statistic.ThreePoints_Attempted = int.Parse(match.Groups[7].Value.Split('/')[1]);

                // Free Throws
                statistic.FreeThrows_Made = int.Parse(match.Groups[9].Value.Split('/')[0]);
                statistic.FreeThrows_Attempted = int.Parse(match.Groups[9].Value.Split('/')[1]);

                // Other Statistics
                statistic.OffensiveRebounds = int.Parse(match.Groups[10].Value);
                statistic.DeffensiveRebounds = int.Parse(match.Groups[11].Value);
                statistic.Assists = int.Parse(match.Groups[12].Value);
                statistic.Turnovers = int.Parse(match.Groups[13].Value);
                statistic.Steals = int.Parse(match.Groups[14].Value);
                statistic.Blocks = int.Parse(match.Groups[15].Value);
                statistic.PersonalFaul = int.Parse(match.Groups[16].Value);
                statistic.FaulDrawned = int.Parse(match.Groups[17].Value);
                statistic.PlusMinus = int.Parse(match.Groups[18].Value);
                statistic.Efficiency = int.Parse(match.Groups[19].Value);
                statistic.Points = int.Parse(match.Groups[20].Value);

                playerStatistics.Add(statistic);
            }

            return playerStatistics;
        }
    }
}
