using Etrosbasket.Data.Enums;

namespace Etrosbasket.ViewModels.Home
{
    public class PlayerHome
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public string MainPhoto { get; set; }
    }
}
