using System.Collections.Generic;
using System.Net;
using FluentValidator;
using MongoAPI.Domain.Entities;

namespace MongoAPI.Domain.Commands.Results.Users
{
	public class GetUserCommandResult : CommandResult
	{
		public GetUserCommandResult() { }
		public GetUserCommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications = null) : base(code, notifications) { }
		public GetUserCommandResult(User user)
		{
			Name = user.Name;
			Email = user.Email.Address;
		}

		public string Name { get; private set; }
		public string Email { get; private set; }
	}
}