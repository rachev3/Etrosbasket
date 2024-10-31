using Etrosbasket.Models;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Data.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerService(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public async Task<List<Player>> GetAll()
        {
            var result = await dbContext.Players.ToListAsync();
            return result;
        }

        public async Task<Player> GetById(int playerId)
        {
            var result = await dbContext.Players.Include(x=>x.Statistics).FirstOrDefaultAsync(p=>p.PlayerId == playerId);
            return result;
        }
        public async Task Add(Player player)
        {
            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Player> Update(int id, Player player)
        {
            dbContext.Update(player);
            await dbContext.SaveChangesAsync();
            return player;
        }

        public async Task Delete(int id)                                           
        {
            var result = await dbContext.Players.FirstOrDefaultAsync(r => r.PlayerId == id);
            dbContext.Players.Remove(result);
            await dbContext.SaveChangesAsync();
        }

    }
}
