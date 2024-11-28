using Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics;

namespace Etrosbasket.Areas.Admin.ViewModels.Players
{
    public class PlayerEditViewModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string BornYear { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string PictureURL { get; set; }
        public List<PlayerStatisticViewModel> Statistics { get; set; }
    }
}
