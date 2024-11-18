namespace Etrosbasket.Models.ViewModels
{
    public class PlayerAverageStatistic
    {
        public PlayerAverageStatistic(List<PlayerStatistic> playerStatistics)
        {
            Minutes = TimeSpan.FromTicks((long)playerStatistics.Average(s => s.Minutes.Ticks)).ToString("mm\\:ss");
            FieldGoals_Made = playerStatistics.Average(s => s.FieldGoals_Made);
            FieldGoals_Attempted = playerStatistics.Average(s => s.FieldGoals_Attempted);
            FieldGoalsPercentage = CalculatePercentage(FieldGoals_Made, FieldGoals_Attempted);

            TwoPoints_Made = playerStatistics.Average(s => s.TwoPoints_Made);
            TwoPoints_Attempted = playerStatistics.Average(s => s.TwoPoints_Attempted);
            TwoPointsPercentage = CalculatePercentage(TwoPoints_Made, TwoPoints_Attempted);

            ThreePoints_Made = playerStatistics.Average(s => s.ThreePoints_Made);
            ThreePoints_Attempted = playerStatistics.Average(s => s.ThreePoints_Attempted);
            ThreePointsPercentage = CalculatePercentage(ThreePoints_Made, ThreePoints_Attempted);

            FreeThrows_Made = playerStatistics.Average(s => s.FreeThrows_Made);
            FreeThrows_Attempted = playerStatistics.Average(s => s.FreeThrows_Attempted);
            FreeThrowsPercentage = CalculatePercentage(FreeThrows_Made, FreeThrows_Attempted);

            OffensiveRebounds = playerStatistics.Average(s => s.OffensiveRebounds);
            DeffensiveRebounds = playerStatistics.Average(s => s.DeffensiveRebounds);
            TotalRebounds = OffensiveRebounds + DeffensiveRebounds;

            Assists = playerStatistics.Average(s => s.Assists);
            Turnovers = playerStatistics.Average(s => s.Turnovers);
            Steals = playerStatistics.Average(s => s.Steals);
            Blocks = playerStatistics.Average(s => s.Blocks);

            PersonalFouls = playerStatistics.Average(s => s.PersonalFaul);
            FaulsDrawned = playerStatistics.Average(s => s.FaulDrawned);

            PlusMinus = playerStatistics.Average(s => s.PlusMinus);
            Efficiency = playerStatistics.Average(s => s.Efficiency);
            Points = playerStatistics.Average(s => s.Points);
        }

        public string Minutes { get; set; }

        public double FieldGoals_Made { get; set; }
        public double FieldGoals_Attempted { get; set; }
        public double FieldGoalsPercentage { get; set; }

        public double TwoPoints_Made { get; set; }
        public double TwoPoints_Attempted { get; set; }
        public double TwoPointsPercentage { get; set; }

        public double ThreePoints_Made { get; set; }
        public double ThreePoints_Attempted { get; set; }
        public double ThreePointsPercentage { get; set; }

        public double FreeThrows_Made { get; set; }
        public double FreeThrows_Attempted { get; set; }
        public double FreeThrowsPercentage { get; set; }

        public double OffensiveRebounds { get; set; }
        public double DeffensiveRebounds { get; set; }
        public double TotalRebounds { get; set; }

        public double Assists { get; set; }
        public double Turnovers { get; set; }
        public double Steals { get; set; }
        public double Blocks { get; set; }

        public double PersonalFouls { get; set; }
        public double FaulsDrawned { get; set; }

        public double PlusMinus { get; set; }
        public double Efficiency { get; set; }
        public double Points { get; set; }
        private double CalculatePercentage(double made, double attempted)
        {
            return attempted > 0 ? Math.Round((made / attempted) * 100, 1) : 0;
        }
    }
}
