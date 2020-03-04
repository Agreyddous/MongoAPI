namespace MongoAPI.Domain.Commands.Users
{
	public class GetUserCommand : ICommand
	{
		public GetUserCommand(string userId) => UserId = userId;

		public string UserId { get; private set; }
	}
}