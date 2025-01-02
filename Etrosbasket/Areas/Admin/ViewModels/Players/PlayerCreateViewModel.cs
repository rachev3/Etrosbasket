using Etrosbasket.Data.Enums;

namespace Etrosbasket.Areas.Admin.ViewModels.Players
{
    public class PlayerCreateViewModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Number {  get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public bool IsStartingFive { get; set; }
        public int Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
    }
}
