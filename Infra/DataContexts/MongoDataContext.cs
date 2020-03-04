using MongoAPI;
using MongoDB.Driver;

namespace ORQ.Infra.CommonContext.DataContexts
{
	public class MongoDataContext
	{
		public MongoDataContext() => Database = new MongoClient(Settings.ConnectionString).GetDatabase(Settings.DatabaseName);

		public IMongoDatabase Database { get; private set; }
	}
}