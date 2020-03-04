using FluentValidator.Validation;

namespace MongoAPI.Domain.ValueObjects
{
	public class Email : ValueObject
	{
		protected Email() { }
		public Email(string address)
		{
			Address = address?.Trim();

			_validate();
		}

		public string Address { get; private set; }

		protected override void _validate()
		{
			ValidationContract contract = new ValidationContract();

			contract.IsNotNullOrEmpty(Address, nameof(Address), "Null or Empty")
				.IsEmail(Address, nameof(Address), "Not Valid");

			AddNotifications(contract);
		}
	}
}