namespace Etrosbasket.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string BornYear { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? PictureURL { get; set; }
        public List<PlayerStatistic>? Statistics { get; set; }

    }
}
