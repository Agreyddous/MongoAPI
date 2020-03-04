using System.Net;
using FluentValidator;
using MongoAPI.Domain.Commands.Results.Defaults;
using MongoAPI.Domain.Commands.Results.Users;
using MongoAPI.Domain.Commands.Users;
using MongoAPI.Domain.Entities;
using MongoAPI.Domain.ValueObjects;
using MongoAPI.Infra.Repositories;

namespace MongoAPI.Domain.Handlers
{
	public class UserCommandHandler : Notifiable,
									ICommandHandler<GetUserCommand, GetUserCommandResult>,
									ICommandHandler<CreateUserCommand, CreateCommandResult<string>>
	{
		private UserRepository _userRepository;

		public UserCommandHandler(UserRepository userRepository) => _userRepository = userRepository;

		public GetUserCommandResult Handle(GetUserCommand command)
		{
			throw new System.NotImplementedException();
		}

		public CreateCommandResult<string> Handle(CreateUserCommand command)
		{
			CreateCommandResult<string> result = new CreateCommandResult<string>();

			if (string.IsNullOrEmpty(command.Name))
				AddNotification(nameof(command.Name), "Null or Empty");

			if (string.IsNullOrEmpty(command.Email))
				AddNotification(nameof(command.Email), "Null or Empty");

			if (string.IsNullOrEmpty(command.Password))
				AddNotification(nameof(command.Password), "Null or Empty");

			if (Valid)
			{
				User user = new User(command.Name,
						 new Email(command.Email),
						 new Password(command.Password));

				AddNotifications(user);

				if (Valid)
				{
					_userRepository.Add(user);

					if (_userRepository.Valid)
						result = new CreateCommandResult<string>(user.Id.ToString());
				}

				else
					result = new CreateCommandResult<string>(HttpStatusCode.BadRequest, Notifications);
			}

			else
				result = new CreateCommandResult<string>(HttpStatusCode.BadRequest, Notifications);

			return result;
		}
	}
}