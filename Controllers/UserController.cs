using Microsoft.AspNetCore.Mvc;
using MongoAPI.Domain.Commands.Results.Defaults;
using MongoAPI.Domain.Commands.Users;
using MongoAPI.Domain.Handlers;

namespace MongoAPI.Controllers
{
	public class UserController : DefaultController
	{
		public UserController(UserCommandHandler handler) : base(handler) { }

		[HttpPost]
		[Route("/V1/Users")]
		public CreateCommandResult<string> Create([FromBody] CreateUserCommand command) => Execute<CreateUserCommand, CreateCommandResult<string>>(command ?? new CreateUserCommand());
	}
}