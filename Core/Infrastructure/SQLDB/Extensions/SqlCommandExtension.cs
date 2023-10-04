using Microsoft.Data.SqlClient;
using Project.Core.Infrastructure.SQLDB.Exceptions;

namespace Project.Core.Infrastructure.SQLDB.Extensions;

public static class SqlCommandExtension
{
    private static readonly Random Random = new(60);

    private static readonly List<int> TransientErrorNumbers =
        new() { 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 };

    [Obsolete]
    public static TResult RetryExecute<TResult>(this SqlCommand command, Func<TResult> func)
    {
        const int maxRetryCnt = 3;
        SqlException? exception = null;
        for (int count = 1; count <= maxRetryCnt; count++)
        {
            try
            {
                return func();
            }
            catch (SqlException ex)
            {
                exception = ex;
                if (count == maxRetryCnt || !TransientErrorNumbers.Contains(ex.Number))
                {
                    throw ex;
                }
            }

            Task.Delay(Random.Next(5, 60) * 1000);
        }

        throw new SqlExecuteException("execute sql error", exception!);
    }

    public static void IsOptimisticLocked(this int result)
    {
        if (result == 0)
        {
            throw new OptimisticLockingException("optimistic error");
        }
    }
}