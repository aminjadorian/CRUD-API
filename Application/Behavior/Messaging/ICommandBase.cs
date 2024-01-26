using MediatR;

namespace Application.Behavior.Messaging;

public interface ICommand : IRequest, ICommandBase
{

}
public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{

}
public interface ICommandBase
{
}
