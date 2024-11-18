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
        public string FormattedMinutes => Minutes.ToString("mm\\:ss");

        public int FieldGoals_Made => TwoPoints_Made + ThreePoints_Made;
        public int FieldGoals_Attempted => TwoPoints_Attempted + ThreePoints_Attempted;
        public double FieldGooalsPercentage
        {
            get
            {
                return FieldGoals_Attempted == 0 ? 0 : Math.Round((double)FieldGoals_Made / FieldGoals_Attempted * 100, 1);
            }
        }


        public int TwoPoints_Made { get; set; }
        public int TwoPoints_Attempted { get; set; }
        public double TwoPointsPercentage
        {
            get
            {
                return TwoPoints_Attempted == 0 ? 0 : Math.Round((double)TwoPoints_Made / TwoPoints_Attempted * 100, 1);
            }
        }

        public int ThreePoints_Made { get; set; }
        public int ThreePoints_Attempted { get; set; }
        public double ThreePointsPercentage
        {
            get
            {
                return ThreePoints_Attempted == 0 ? 0 : Math.Round((double)ThreePoints_Made / ThreePoints_Attempted * 100, 1);
            }
        }

        public int FreeThrows_Made { get; set; }
        public int FreeThrows_Attempted { get; set; }
        public double FreeThrowsPercentage
        {
            get
            {
                return FreeThrows_Attempted == 0 ? 0 : Math.Round((double)FreeThrows_Made / FreeThrows_Attempted * 100, 1);
            }
        }

        public int OffensiveRebounds {  get; set; }
        public int DeffensiveRebounds { get; set; }
        public int TotalRebounds => OffensiveRebounds + DeffensiveRebounds;

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
