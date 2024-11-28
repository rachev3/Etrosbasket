using Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics;
using Etrosbasket.Models;
namespace Etrosbasket.Areas.Admin.ViewModels.Players
{
    public class PlayerDetailsViewModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string PictureURL { get; set; }
        public List<PlayerStatisticViewModel> Statistics { get; set; }
    }
}
