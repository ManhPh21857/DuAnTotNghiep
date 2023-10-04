using MediatR;

namespace Project.Core.ApplicationService.Commands;

public abstract class CommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : class, ICommand<TCommandResult>
{
    public abstract Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken);
}