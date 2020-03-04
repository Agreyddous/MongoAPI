using FluentValidator;
using MongoDB.Bson;

namespace MongoAPI.Domain.Entities
{
	public abstract class Entity : Notifiable
	{
		public ObjectId Id { get; private set; }
	}
}