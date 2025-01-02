using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Models;
using Etrosbasket.ViewModels.Home;

namespace Etrosbasket.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerListViewModel> GetAll();
        Task<List<PlayerHome>> GetStartingFive();
        Task<PlayerDetailsViewModel> GetPlayerDetails(int playerId);
        Task Add(Player player);
        Task<PlayerEditViewModel> GetPlayerEdit(int playerId);
        Task Update(PlayerEditViewModel viewModel);
        Task Delete(int id);

    }
}
