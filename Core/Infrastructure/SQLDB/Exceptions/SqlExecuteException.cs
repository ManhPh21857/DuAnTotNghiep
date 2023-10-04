namespace Project.Core.Infrastructure.SQLDB.Exceptions;

public class SqlExecuteException : Exception
{
    public const string CommonErrorMessage = "SQl error";

    public SqlExecuteException(string message) : base(message)
    {
    }

    public SqlExecuteException(string message, Exception inner) : base(message, inner)
    {
    }
}