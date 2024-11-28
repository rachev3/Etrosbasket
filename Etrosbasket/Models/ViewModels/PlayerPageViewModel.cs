using Etrosbasket.Areas.Admin.ViewModels.Players;
using Humanizer;

namespace Etrosbasket.Models.ViewModels
{
    public class PlayerPageViewModel
    {
        public PlayerPageViewModel(PlayerDetailsViewModel player)
        {
            PlayerAverageStatistic = new PlayerAverageStatistic(player.Statistics);
        }
        public PlayerDetailsViewModel PlayerDetails { get; set; }
        public PlayerAverageStatistic PlayerAverageStatistic { get; set; }
    }
}
