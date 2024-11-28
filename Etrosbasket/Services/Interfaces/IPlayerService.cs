using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Models;

namespace Etrosbasket.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerListViewModel> GetAll();
        Task<PlayerEditViewModel> GetPlayerEdit(int playerId);
        Task<PlayerDetailsViewModel> GetPlayerDetails(int playerId);
        Task Add(Player player);
        Task<Player> Update(int id, Player repair);
        Task Delete(int id);

    }
}
