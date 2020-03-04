using System;
using System.Security.Cryptography;
using System.Text;
using FluentValidator.Validation;

namespace MongoAPI.Domain.ValueObjects
{
	public class Password : ValueObject
	{
		protected Password() { }
		public Password(string text)
		{
			_validate();

			if (Valid)
			{
				Salt = _generateSalt();
				Text = _hash(text.Trim(), Salt);
			}
		}

		public bool Login(string password) => Text == _hash(password, Salt);

		public string Text { get; private set; }
		public string Salt { get; private set; }

		private string _hash(string text, string salt)
		{
			Rfc2898DeriveBytes Rfc2898DeriveBytes = new Rfc2898DeriveBytes(text, Encoding.Default.GetBytes(salt), 1000);

			return BitConverter.ToString(Rfc2898DeriveBytes.GetBytes(20));
		}

		private string _generateSalt()
		{
			string salt = string.Empty;

			byte[] bytes = new byte[128 / 8];
			using (RandomNumberGenerator keyGenerator = RandomNumberGenerator.Create())
			{
				keyGenerator.GetBytes(bytes);
				salt = BitConverter.ToString(bytes).Replace("-", "").ToLower();
			}

			return salt;
		}

		protected override void _validate()
		{
			ValidationContract contract = new ValidationContract();

			contract.IsNotNullOrEmpty(Text, nameof(Password), "Null or Empty")
				.HasMinLen(Text ?? string.Empty, 5, nameof(Password), "Too Short");

			AddNotifications(contract);
		}
	}
}