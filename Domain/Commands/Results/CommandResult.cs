using System.Collections.Generic;
using System.Net;
using FluentValidator;

namespace MongoAPI.Domain.Commands.Results
{
	public class CommandResult
	{
		private HttpStatusCode _code;

		public CommandResult() : this(HttpStatusCode.InternalServerError) { }
		public CommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications = null)
		{
			_code = code;
			Notifications = notifications;
		}

		public IReadOnlyCollection<Notification> Notifications { get; private set; }

		public HttpStatusCode Code() => _code;
	}
}