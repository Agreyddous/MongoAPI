using FluentValidator.Validation;
using MongoAPI.Domain.ValueObjects;

namespace MongoAPI.Domain.Entities
{
	public class User : Entity
	{
		protected User() { }
		public User(string name,
			  Email email,
			  Password password)
		{
			Name = name;
			Email = email;
			Password = password;

			_validate();
		}

		public string Name { get; private set; }
		public Email Email { get; private set; }
		public Password Password { get; private set; }

		private void _validate()
		{
			ValidationContract contract = new ValidationContract();

			contract.IsNotNullOrEmpty(Name, nameof(Name), "Can't be null or Empty");

			AddNotifications(contract);
			AddNotifications(Email);
			AddNotifications(Password);
		}
	}
}