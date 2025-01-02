using Etrosbasket.Data.Enums;
using Etrosbasket.Models;
namespace Etrosbasket.Areas.Admin.ViewModels.Players
{
    public class PlayerViewModel
    {
        public int PlayerId { get; set; }
        public int Number {  get; set; }
        public string Name { get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public int Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
    }
}

