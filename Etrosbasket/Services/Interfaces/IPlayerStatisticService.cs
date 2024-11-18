using Etrosbasket.Models;

namespace Etrosbasket.Services.Interfaces
{
    public interface IPlayerStatisticService
    {
        Task<List<PlayerStatistic>> GetAll();
        Task<List<PlayerStatistic>> GetByPlayerId(int playerId);
        Task Add(PlayerStatistic playerStatistic);
    }
}
