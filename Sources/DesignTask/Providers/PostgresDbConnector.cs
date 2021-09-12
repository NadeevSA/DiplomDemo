using System.Data;
using System.Threading.Tasks;
using Dapper;
using DesignTask.Providers;
using Npgsql;

namespace KnowledgeUberization.Providers
{
    public class PostgresDbConnector : IDbConnector
    {
        private string _connectionSttring;
        private string _connection;

        static PostgresDbConnector()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        
        public PostgresDbConnector(string connectionSttring)
        {
            _connectionSttring = connectionSttring;
        }

        public IDbConnection Connect(bool removeFromPoolOnDispose = false)
        {
            var connection = new NpgsqlConnection(_connectionSttring);
            connection.Open();
            return connection;
        }

        public async Task<IDbConnection> ConnectAsync(bool removeFromPoolDispose = false)
        {
            var connection = new NpgsqlConnection(_connectionSttring);
            await connection.OpenAsync();
            return connection;
        }
    }
}