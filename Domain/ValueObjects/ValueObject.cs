using FluentValidator;

namespace MongoAPI.Domain.ValueObjects
{
	public abstract class ValueObject : Notifiable
	{
		protected abstract void _validate();
	}
}