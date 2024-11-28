using Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etrosbasket.Models
{
    public class PlayerStatistic
    {
        public PlayerStatistic()
        {
            
        }
        public PlayerStatistic(PlayerStatisticViewModel viewModel)
        {
            StatisticId = viewModel.StatisticId;
            TeamAgainst = viewModel.TeamAgainst;
            Date = viewModel.Date;
            Minutes = TimeSpan.ParseExact(viewModel.Minutes, @"mm\:ss", null);
            TwoPoints_Made = viewModel.TwoPoints_Made;
            TwoPoints_Attempted = viewModel.TwoPoints_Attempted;
            ThreePoints_Made = viewModel.ThreePoints_Made;
            ThreePoints_Attempted = viewModel.ThreePoints_Attempted;
            FreeThrows_Made = viewModel.FreeThrows_Made;
            FreeThrows_Attempted = viewModel.FreeThrows_Attempted;
            OffensiveRebounds = viewModel.OffensiveRebounds;
            DeffensiveRebounds = viewModel.DeffensiveRebounds;
            Assists = viewModel.Assists;
            Turnovers = viewModel.Turnovers;
            Steals = viewModel.Steals;
            Blocks = viewModel.Blocks;
            PersonalFaul = viewModel.PersonalFaul;
            FaulDrawned = viewModel.FaulDrawned;
            PlusMinus = viewModel.PlusMinus;
            Efficiency = viewModel.Efficiency;
            Points = viewModel.Points;
        }
        [Key]
        public int StatisticId { get; set; }
        public string TeamAgainst { get; set; }
        public DateTime Date {  get; set; }
        public TimeSpan Minutes { get; set; }
     
        public int TwoPoints_Made { get; set; }
        public int TwoPoints_Attempted { get; set; }

        public int ThreePoints_Made { get; set; }
        public int ThreePoints_Attempted { get; set; }

        public int FreeThrows_Made { get; set; }
        public int FreeThrows_Attempted { get; set; }
  
        public int OffensiveRebounds {  get; set; }
        public int DeffensiveRebounds { get; set; }

        public int Assists { get; set; }
        public int Turnovers { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int PersonalFaul {  get; set; }
        public int FaulDrawned { get; set; }
        public int PlusMinus { get; set; }
        public int Efficiency { get; set; }
        public int Points { get; set; }


        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
