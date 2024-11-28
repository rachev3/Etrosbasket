using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Models;

namespace Etrosbasket.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerListViewModel> GetAll();
        Task<PlayerDetailsViewModel> GetPlayerDetails(int playerId);
        Task Add(Player player);
        Task<PlayerEditViewModel> GetPlayerEdit(int playerId);
        Task Update(PlayerEditViewModel viewModel);
        Task Delete(int id);

    }
}
