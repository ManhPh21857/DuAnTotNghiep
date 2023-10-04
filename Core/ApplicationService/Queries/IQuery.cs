using MediatR;

namespace Project.Core.ApplicationService.Queries;

public interface IQuery<TQueryResult> : IRequest<TQueryResult>
{
}