using Etrosbasket.Models;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Data.Services
{
    public class PlayerStatisticService : IPlayerStatisticService
    {
        private readonly ApplicationDbContext dbContext;
        public PlayerStatisticService(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public async Task<List<PlayerStatistic>> GetAll()
        {
            var result = await dbContext.PlayerStatistics.ToListAsync();
            return result;
        }

        public async Task<List<PlayerStatistic>> GetByPlayerId(int playerId)
        {
            var result =  await dbContext.PlayerStatistics.Where(ps=> ps.PlayerId == playerId).ToListAsync();
            return result;
        }
    }
}
