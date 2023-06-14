using DapperAPI.CustomModels;

namespace DapperAPI.Interfaces
{
    public interface IDapperRepository
    {
        public Task<List<Player>> GetPlayers();
        public Task<Player> GetPlayer(int? id);
        public Task<int> CreatePlayer(Player player);
        public Task<int> UpdatePlayer(Player player);
        public Task<int> DeletePlayer(int id);
        public Task<CustomPlayer> GetPlayerInfo(int id);
    }
}
