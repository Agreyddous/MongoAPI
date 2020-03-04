using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidator;
using MongoAPI.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using ORQ.Infra.CommonContext.DataContexts;

namespace MongoAPI.Infra.Repositories
{
	public abstract class Repository<TEntity> : Notifiable where TEntity : Entity
	{
		protected readonly IMongoCollection<TEntity> collection;

		public Repository(string collectionName, MongoDataContext dataContext) => this.collection = dataContext.Database.GetCollection<TEntity>(collectionName);

		public TEntity Add(TEntity entity)
		{
			try
			{
				collection.InsertOne(entity);
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}

			return entity;
		}

		public void Remove(TEntity entity)
		{
			try
			{
				collection.DeleteOne(collectionEntity => collectionEntity.Id == entity.Id);
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}
		}

		public bool Exists(params object[] ids)
		{
			bool result = false;

			try
			{
				result = collection.Find<TEntity>(entity => entity.Id == (ObjectId)ids[0]).Any();
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}

			return result;
		}

		public TEntity Get(params object[] ids)
		{
			TEntity result = null;

			try
			{
				result = collection.Find<TEntity>(entity => entity.Id == (ObjectId)ids[0]).FirstOrDefault();
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}

			return result;
		}

		public IEnumerable<TEntity> GetAll()
		{
			IEnumerable<TEntity> entities = null;

			try
			{
				entities = collection.AsQueryable();
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}

			return entities;
		}

		public void Update(TEntity entity)
		{
			try
			{
				collection.ReplaceOne(collectionEntity => collectionEntity.Id == entity.Id, entity);
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}
		}

		public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> searchExpression)
		{
			IEnumerable<TEntity> result = null;

			try
			{
				result = collection.AsQueryable().Where(searchExpression);
			}
			catch (Exception exception)
			{
				AddNotification("Error", exception.Message);
			}

			return result;
		}
	}
}