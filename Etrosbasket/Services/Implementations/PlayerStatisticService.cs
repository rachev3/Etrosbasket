using Etrosbasket.Data;
using Etrosbasket.Models;
using Etrosbasket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Services.Implementations
{
    public class PlayerStatisticService : IPlayerStatisticService
    {
        private readonly ApplicationDbContext dbContext;
        public PlayerStatisticService(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public async Task Add(PlayerStatistic playerStatistic)
        {
            await dbContext.PlayerStatistics.AddAsync(playerStatistic);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<PlayerStatistic>> GetAll()
        {
            var result = await dbContext.PlayerStatistics.ToListAsync();
            return result;
        }

        public async Task<List<PlayerStatistic>> GetByPlayerId(int playerId)
        {
            var result = await dbContext.PlayerStatistics.Where(ps => ps.PlayerId == playerId).ToListAsync();
            return result;
        }


    }
}
