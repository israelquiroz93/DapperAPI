using Dapper;
using DapperAPI.CustomModels;
using DapperAPI.Interfaces;
using System.Data;

namespace DapperAPI.Repositories
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _dbContext;

        public DapperRepository(DapperContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<int> CreatePlayer(Player player)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                //var parameters = new DynamicParameters(new { playerId = player.PlayerId, ParameterDirection.Output });

                var affectedRows = await connection.ExecuteAsync("INSERT INTO Player (playerName, teamId) VALUES (@playerName, @teamId)", new { playerName = player.PlayerName, teamId = player.TeamId });

                //could do a get the id to see if it was created, or just check affected rows

                return affectedRows;
            }
        }

        public async Task<int> DeletePlayer(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Player WHERE playerId=@id", new { id = id });
                return affectedRows;
            }
        }

        public async Task<List<Player>> GetPlayers()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var players = await connection.QueryAsync<Player, Team, Player>("SELECT * FROM Player as p Join Team as t on t.teamId = p.teamId", (player, team) =>
                {
                    player.Team = team;
                    return player;
                }
                , splitOn: "teamId");
                return players.ToList();
            }
        }

        public async Task<Player> GetPlayer(int? id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                //var dictionary = new Dictionary<string, object>
                //{
                //    { "@ProductId", 1 }
                //};
                //var parameters = new DynamicParameters(dictionary);
                var parameters = new DynamicParameters(new { playerId = id });

                var players = await connection.QueryFirstAsync<Player>("SELECT * FROM Player WHERE playerId=@playerId", parameters);
                return players;
            }
        }

        public async Task<int> UpdatePlayer(Player player)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync($"UPDATE Player SET playerName=@playerName WHERE playerId=@Id", new { playerName = player.PlayerName, Id = player.PlayerId });
                return affectedRows;
            }
        }


        public async Task<CustomPlayer> GetPlayerInfo(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var procedure = "GetPlayerInfo";
                var parameters = new DynamicParameters(new { Id = id });
                var results = await connection.QueryFirstAsync<CustomPlayer>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return results;
            }
        }


        public async Task<int> CreatePlayerTransaction(Player player)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var affectedRow = await connection.ExecuteAsync("INSERT INTO Player (playerName, teamId) VALUES (@playerName, @teamId)", new { playerName = "new player 1", teamId = player.TeamId });

                    var affectedRow2 = await connection.ExecuteAsync("INSERT INTO Player (playerName, teamId) VALUES (@playerName, @teamId)", new { playerName = "new player 2", teamId = player.TeamId });

                    transaction.Commit();

                    return affectedRow;
                }
            }
        }
    }
}
