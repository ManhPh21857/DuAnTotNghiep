using MediatR;

namespace Project.Core.ApplicationService.Commands;

public interface ICommand<TCommandResult> : IRequest<TCommandResult>
{
}