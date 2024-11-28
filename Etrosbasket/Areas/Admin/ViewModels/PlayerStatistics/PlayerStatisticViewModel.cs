using Etrosbasket.Models;

namespace Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics
{
    public class PlayerStatisticViewModel
    {
        public PlayerStatisticViewModel(PlayerStatistic playerStatistic)
        {
            StatisticId = playerStatistic.StatisticId;
            TeamAgainst = playerStatistic.TeamAgainst;
            Date = playerStatistic.Date;
            Minutes = playerStatistic.Minutes.ToString("mm\\:ss");
            FieldGoals_Made = playerStatistic.TwoPoints_Made + playerStatistic.ThreePoints_Made;
            FieldGoals_Attempted = playerStatistic.TwoPoints_Attempted + playerStatistic.ThreePoints_Attempted;
            TwoPoints_Attempted = playerStatistic.TwoPoints_Attempted;
            TwoPoints_Made = playerStatistic.TwoPoints_Made;
            ThreePoints_Attempted = playerStatistic.ThreePoints_Attempted;
            ThreePoints_Made = playerStatistic.ThreePoints_Made;
            FreeThrows_Attempted = playerStatistic.FreeThrows_Attempted;
            FreeThrows_Made = playerStatistic.FreeThrows_Made;
            OffensiveRebounds = playerStatistic.OffensiveRebounds;
            DeffensiveRebounds = playerStatistic.DeffensiveRebounds;
            Assists = playerStatistic.Assists;
            Turnovers = playerStatistic.Turnovers;
            Steals = playerStatistic.Steals;
            Blocks = playerStatistic.Blocks;
            PersonalFaul = playerStatistic.PersonalFaul;
            FaulDrawned = playerStatistic.FaulDrawned;
            PlusMinus = playerStatistic.PlusMinus;
            Efficiency = playerStatistic.Efficiency;
            Points = playerStatistic.Points;
        }

        public int StatisticId { get; set; }
        public string TeamAgainst { get; set; }
        public DateTime Date { get; set; }
        public string Minutes { get; set; }
        //public string FormattedMinutes => Minutes.ToString("mm\\:ss");

        public int FieldGoals_Made { get; set; }
        public int FieldGoals_Attempted {  get; set; }
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

        public int OffensiveRebounds { get; set; }
        public int DeffensiveRebounds { get; set; }
        public int TotalRebounds => OffensiveRebounds + DeffensiveRebounds;

        public int Assists { get; set; }
        public int Turnovers { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int PersonalFaul { get; set; }
        public int FaulDrawned { get; set; }
        public int PlusMinus { get; set; }
        public int Efficiency { get; set; }
        public int Points { get; set; }



    }
}
