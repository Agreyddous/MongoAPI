using MongoAPI.Domain.Commands;
using MongoAPI.Domain.Commands.Results;

namespace MongoAPI.Domain.Handlers
{
	public interface ICommandHandler
	{
		delegate CommandResult Handle(ICommand command);
	}

	public interface ICommandHandler<TCommand, TCommandResult> : ICommandHandler where TCommand : ICommand where TCommandResult : CommandResult
	{
		new TCommandResult Handle(TCommand command);
	}
}