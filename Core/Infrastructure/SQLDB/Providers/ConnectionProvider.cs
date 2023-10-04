using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Project.Core.Infrastructure.SQLDB.Providers;

public sealed class ConnectionProvider : IDisposable
{
    private readonly string connectionString;
    private SqlConnection? connection;
    private SqlRetryLogicOption options;
    private readonly ILogger<ConnectionProvider> logger;

    public ConnectionProvider(IConfiguration configuration, ILogger<ConnectionProvider> newLogger)
    {
        connectionString = configuration.GetConnectionString("Business");
        AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableRetryLogic", true);
        options = new SqlRetryLogicOption()
        {
            NumberOfTries = int.Parse(configuration["DBRetry:NumberOfTries"]),
            MaxTimeInterval = TimeSpan.FromSeconds(int.Parse(configuration["DBRetry:MaxTimeInterval"])),
            DeltaTime = TimeSpan.FromSeconds(int.Parse(configuration["DBRetry:DeltaTime"])),
            TransientErrors = new List<int>() { 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 }
        };
        logger = newLogger;
    }

    public async Task<SqlConnection> Connect()
    {
        var provider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(options);
        provider.Retrying += (s, e) =>
        {
            var attempts = e.RetryCount + 1;
            logger.LogError($"attempt {attempts} - current delay time:{e.Delay} \n");
            if (e.Exceptions[^1] is SqlException ex)
            {
                logger.LogError($"{ex.Number}-{ex.Message}\n");
            }
            else
            {
                logger.LogError($"{e.Exceptions[^1].Message}\n");
            }

            if (e.RetryCount == provider.RetryLogic.NumberOfTries - 1)
            {
                logger.LogError("This is the last chance to execute the command before throwing the exception.");
            }
        };
        try
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                return connection;
            }

            connection = this.CreateConnection();
            connection.RetryLogicProvider = provider;
            await connection.OpenAsync();
            return connection;
        }
        catch (Exception ex)
        {
            throw new Exception("connect error business db", ex);
        }
    }

    private SqlConnection CreateConnection()
    {
        return new(connectionString);
    }

    public void Dispose()
    {
        if (connection != null)
        {
            connection.Close();
            connection.Dispose();
        }
    }
}