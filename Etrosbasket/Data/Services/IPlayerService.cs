﻿using Etrosbasket.Models;

namespace Etrosbasket.Data.Services
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAll();
        Task<Player> GetById(int playerId);
        Task Add(Player player);
        Task<Player> Update(int id, Player repair);
        Task Delete(int id);

    }
}
