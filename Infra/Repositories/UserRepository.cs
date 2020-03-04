using MongoAPI.Domain.Entities;
using ORQ.Infra.CommonContext.DataContexts;

namespace MongoAPI.Infra.Repositories
{
	public class UserRepository : Repository<User>
	{
		public UserRepository(MongoDataContext dataContext) : base("Users", dataContext) { }
	}
}