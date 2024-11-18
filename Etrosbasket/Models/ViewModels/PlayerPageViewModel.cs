using Humanizer;

namespace Etrosbasket.Models.ViewModels
{
    public class PlayerPageViewModel
    {
        public PlayerPageViewModel(Player player)
        {
            PlayerId = player.PlayerId;
            Name = player.Name;
            Age =  DateTime.Now.Year - int.Parse(player.BornYear);
            Weight = player.Weight;
            Height = player.Height;
            PictureURL = player.PictureURL;
            StatisticList = player.Statistics.OrderByDescending(x=>x.Date).ToList();
            PlayerAverageStatistic = new PlayerAverageStatistic(player.Statistics);
        }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? PictureURL { get; set; } 
        public List<PlayerStatistic> StatisticList { get; set; }
        public PlayerAverageStatistic PlayerAverageStatistic { get; set; }
    }
}
