using Etrosbasket.Models;

namespace Etrosbasket.Data.Services
{
    public interface IPlayerStatisticService
    {
        Task<List<PlayerStatistic>> GetAll();
        Task<List<PlayerStatistic>> GetByPlayerId(int playerId);
    }
}
