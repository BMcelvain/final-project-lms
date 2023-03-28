using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Wrappers
{
    public class SqlWrapper : ISqlWrapper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            this._connection = connection;

            return connection;
        }

        public async Task<List<T>> QueryAsync<T>(string sql)
        {
            var result = await this._connection.QueryAsync<T>(sql);

            return result.ToList();        
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object status)
        {
            var result = await this._connection.QueryAsync<T>(sql, status);

            return result.ToList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql)
        {
            var result = await this._connection.QueryFirstOrDefaultAsync<T>(sql);
            return result;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters)
        {
            var result = await this._connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            return result;
        }

        public async Task ExecuteAsync(string sql)
        {
            await this._connection.ExecuteAsync(sql);
        }

        public async Task ExecuteAsync(string sql, DynamicParameters parameters)
        {
            await this._connection.ExecuteAsync(sql, parameters);
        }
    }
}
