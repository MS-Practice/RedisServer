using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
namespace RedisServer.Utilities {
    public class RedisManager {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;
        public RedisManager(string connectionString) {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _database = _connection.GetDatabase();
        }
        //操作 string
        public async Task<bool> SetStringAsync(string key, string value) {
            return await _database.StringSetAsync(key, value);
        }

        public async Task<string> GetStringAsync(string key) {
            return await _database.StringGetAsync(key);
        }

        public async Task SetHashAsync(string key, HashEntry[] keyValues) {
            await _database.HashSetAsync(key, keyValues);
        }

        public async Task<bool> HashInsertAsync(string key, string property, string propertyValue, When when, CommandFlags flags) {
            return await _database.HashSetAsync(key, property, propertyValue, when : when, flags : flags);
        }

        // public async Task<bool> Set<TKey, TValue>(TKey key, TValue value)
        // where TKey : IEquatable<RedisKey>
        //     where TValue : IEquatable<RedisValue> {
        //         return await _database.SetAddAsync(key, value);
        //     }

        public ISubscriber Subscriber => _connection.GetSubscriber();

    }
}