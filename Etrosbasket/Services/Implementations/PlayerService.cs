using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics;
using Etrosbasket.Data;
using Etrosbasket.Models;
using Etrosbasket.Services.Interfaces;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerService(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public async Task<PlayerListViewModel> GetAll()
        {
            var result = await dbContext.Players.ToListAsync();
            PlayerListViewModel viewModel = new PlayerListViewModel();
            viewModel.PlayersList = result.Select(player => new PlayerViewModel
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Age =  DateTime.Now.Year - int.Parse(player.BornYear),
                Weight = player.Weight,
                Height = player.Height,
            }).ToList();
            return viewModel;
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

        public async Task<PlayerDetailsViewModel> GetPlayerDetails(int playerId)
        {
            var player = await dbContext.Players.Include(p => p.Statistics).FirstOrDefaultAsync(p => p.PlayerId == playerId);

            PlayerDetailsViewModel viewModel = new();
            viewModel.PlayerId = playerId;
            viewModel.Name = player.Name;
            viewModel.Age = DateTime.Now.Year - int.Parse(player.BornYear);
            viewModel.Weight = player.Weight;
            viewModel.Height = player.Height;
            viewModel.PictureURL = player.PictureURL;
            viewModel.Statistics = player.Statistics.Select(stat => new PlayerStatisticViewModel(stat)).ToList();

            return viewModel;
        }

        public async Task<PlayerEditViewModel> GetPlayerEdit(int playerId)
        {
            var result = await dbContext.Players.Include(p => p.Statistics).FirstOrDefaultAsync(p => p.PlayerId == playerId);

            PlayerEditViewModel viewModel = new();
            viewModel.PlayerId = result.PlayerId;
            viewModel.Name = result.Name;
            viewModel.BornYear = result.BornYear;
            viewModel.Weight = result.Weight;
            viewModel.Height = result.Height;
            viewModel.PictureURL = result.PictureURL;
            viewModel.Statistics = result.Statistics.Select(stat => new PlayerStatisticViewModel(stat)).ToList();

            return viewModel;
        }
    }
}
