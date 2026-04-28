using System.Data;

namespace XlibraryPro.Infrastructure.Configuration;

public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}