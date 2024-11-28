using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etrosbasket.Models
{
    public class PlayerStatistic
    {
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
