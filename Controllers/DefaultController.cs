using Microsoft.AspNetCore.Mvc;
using MongoAPI.Domain.Commands;
using MongoAPI.Domain.Commands.Results;
using MongoAPI.Domain.Handlers;

namespace MongoAPI.Controllers
{
	public abstract class DefaultController : Controller
	{
		private ICommandHandler _handler;

		public DefaultController(ICommandHandler handler) => _handler = handler;

		protected TCommandResult Execute<TCommand, TCommandResult>(TCommand command) where TCommand : ICommand where TCommandResult : CommandResult, new()
		{
			TCommandResult result = new TCommandResult();

			try
			{
				result = (_handler as ICommandHandler<TCommand, TCommandResult>).Handle(command);
			}
			catch { }

			HttpContext.Response.StatusCode = (int)result.Code();

			return result;
		}
	}
}