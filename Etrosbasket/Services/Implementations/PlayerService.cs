using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Areas.Admin.ViewModels.PlayerStatistics;
using Etrosbasket.Data;
using Etrosbasket.Models;
using Etrosbasket.Services.Interfaces;
using Etrosbasket.ViewModels.Home;
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
                Number = player.Number,
                Name = player.Name,
                Positions = player.Positions,
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

        public async Task Update(PlayerEditViewModel viewModel)
        {
            var player = await dbContext.Players.FirstOrDefaultAsync(p=>p.PlayerId == viewModel.PlayerId);
            player.Name = viewModel.Name;
            player.Positions = viewModel.Positions;
            player.Number = viewModel.Number;
            player.IsStartingFive = viewModel.IsStartingFive;
            player.BornYear = viewModel.BornYear;
            player.Weight = viewModel.Weight;
            player.Height = viewModel.Height;
            player.PictureURL = viewModel.PictureURL;
            player.Statistics = viewModel.Statistics.Select(stat => new PlayerStatistic(stat)).ToList();
            dbContext.Update(player);
            await dbContext.SaveChangesAsync();
          
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
            viewModel.Number = player.Number;
            viewModel.Name = player.Name;
            viewModel.Positions = player.Positions;
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
            viewModel.Number = result.Number;
            viewModel.Name = result.Name;
            viewModel.IsStartingFive = result.IsStartingFive;
            viewModel.Positions = result.Positions;
            viewModel.BornYear = result.BornYear;
            viewModel.Weight = result.Weight;
            viewModel.Height = result.Height;
            viewModel.PictureURL = result.PictureURL;
            viewModel.Statistics = result.Statistics.Select(stat => new PlayerStatisticViewModel(stat)).ToList();

            return viewModel;
        }

        public async Task<List<PlayerHome>> GetStartingFive()
        {
           
            var players = await dbContext.Players
                .Where(p => p.IsStartingFive) 
                .OrderBy(p => p.Number) 
                .Select(p => new PlayerHome
                {
                    Id = p.PlayerId,
                    Name = p.Name,
                    Positions = p.Positions, 
                  
                })
                .ToListAsync();

            return players;
        }

    }
}
