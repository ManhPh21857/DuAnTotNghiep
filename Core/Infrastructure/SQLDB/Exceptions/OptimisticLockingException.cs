namespace Project.Core.Infrastructure.SQLDB.Exceptions;

public class OptimisticLockingException : Exception
{
    public OptimisticLockingException(string message) : base(message)
    {
    }

    public OptimisticLockingException(string message, Exception inner) : base(message, inner)
    {
    }
}