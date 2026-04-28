using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace XlibraryPro.Infrastructure.Configuration;

public class DbConnectionFactory(IConfiguration configuration) : IConnectionFactory
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}