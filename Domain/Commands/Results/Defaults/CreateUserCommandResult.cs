using System.Collections.Generic;
using System.Net;
using FluentValidator;

namespace MongoAPI.Domain.Commands.Results.Defaults
{
	public class CreateCommandResult<TIdType> : CommandResult
	{
		public CreateCommandResult() { }
		public CreateCommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications = null) : base(code, notifications) { }
		public CreateCommandResult(TIdType id) : base(HttpStatusCode.Created)
		{
			Id = id;
		}

		public TIdType Id { get; private set; }
	}
}