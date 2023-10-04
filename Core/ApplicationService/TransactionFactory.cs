using System.Transactions;

namespace Project.Core.ApplicationService;

public static class TransactionFactory
{
    public static TransactionScope Create()
    {
        return Create(TimeSpan.FromSeconds(600));
    }

    public static TransactionScope Create(TimeSpan timeout)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = timeout },
            TransactionScopeAsyncFlowOption.Enabled
        );
    }
}