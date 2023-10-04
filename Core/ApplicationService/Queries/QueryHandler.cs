using MediatR;

namespace Project.Core.ApplicationService.Queries;

public abstract class QueryHandler<TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
{
    public abstract Task<TQueryResult> Handle(TQuery request, CancellationToken cancellationToken);
}