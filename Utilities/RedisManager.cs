using StackExchange.Redis;
namespace RedisServer.Utilities {
    public class RedisManager {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;
        public RedisManager(string connectionString) {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _database = _connection.GetDatabase();
        }
    }
}