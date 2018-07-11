using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StatTrack.BLL.Repositories
{
	/// <summary>
	/// Class to perform data related operations.
	/// </summary>
	/// <typeparam name="T">Model type.</typeparam>
	public sealed class StggRepository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _dbContext;
		private DbSet<T> _dbSet;

		public StggRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}

		#region Any

		/// <summary>
		/// Returns true if theres a record that is being described on the filter.
		/// </summary>
		/// <param name="filter">Decription of records.</param>
		public bool Any(Expression<Func<T, bool>> filter)
		{
			return _dbSet.Any(filter);
		}

		#endregion

		#region Any Async

		/// <summary>
		/// Returns true if theres a record that is being described on the filter.
		/// </summary>
		/// <param name="filter">Decription of records.</param>
		public Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
		{
			return _dbSet.AnyAsync(filter);
		}

		#endregion

		#region Count

		/// <summary>
		/// Counts how many records we have inside the table.
		/// </summary>
		public int Count()
		{
			return _dbSet.Count();
		}

		/// <summary>
		/// Counts how many records we have inside the table.
		/// </summary>
		/// <param name="filter">Decription of records.</param>
		public int Count(Expression<Func<T, bool>> filter)
		{
			return _dbSet.Count(filter);
		}

		#endregion

		#region Count Async

		/// <summary>
		/// Counts how many records we have inside the table.
		/// </summary>
		public Task<int> CountAsync()
		{
			return _dbSet.CountAsync();
		}

		/// <summary>
		/// Counts how many records we have inside the table.
		/// </summary>
		/// <param name="filter">Decription of records.</param>
		public Task<int> CountAsync(Expression<Func<T, bool>> filter)
		{
			return _dbSet.CountAsync(filter);
		}

		#endregion

		#region Get One

		/// <summary>
		/// Get a record from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="includeProperties">Included properties.</param>
		public T GetOne(
			Expression<Func<T, bool>> filter,
			params Expression<Func<T, object>>[] includeProperties)
		{
			// ***********************
			// Set included properties
			// ***********************
			IQueryable<T> query = _dbSet;
			GetPropertyNames(ref query, includeProperties);

			return query.FirstOrDefault(filter);
		}

		#endregion

		#region Get One Async

		/// <summary>
		/// Get a record from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="includeProperties">Included properties.</param>
		public Task<T> GetOneAsync(
			Expression<Func<T, bool>> filter,
			params Expression<Func<T, object>>[] includeProperties)
		{
			// ***********************
			// Set included properties
			// ***********************
			IQueryable<T> query = _dbSet;
			GetPropertyNames(ref query, includeProperties);

			return query.FirstOrDefaultAsync(filter);
		}

		#endregion

		#region Get Many

		/// <summary>
		/// Get multiple records from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="total">Total records to retreive.</param>
		/// <param name="index">0 based page index.</param>
		/// <param name="size">Size per page.</param>
		/// <param name="includeProperties">Included properties.</param>
		public List<T> GetMany(
			Expression<Func<T, bool>> filter,
			out int total,
			int index,
			int size,
			params Expression<Func<T, object>>[] includeProperties)
		{

			var query = GetMany(filter, includeProperties)
				.Skip(size * index)
				.Take(size);

			total = query.Count();

			return query.ToList();
		}

		/// <summary>
		/// Get multiple records from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="includeProperties">Included properties.</param>
		public List<T> GetMany(
			Expression<Func<T, bool>> filter = null,
			params Expression<Func<T, object>>[] includeProperties)
		{
			// ***********************
			// Set included properties
			// ***********************
			IQueryable<T> query = _dbSet;
			GetPropertyNames(ref query, includeProperties);

			if (filter != null)
			{
				query = query.Where(filter);
			}


			return query.ToList();
		}

		#endregion

		#region Get Many Async

		/// <summary>
		/// Get multiple records from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="total">Total records to retreive.</param>
		/// <param name="index">0 based page index.</param>
		/// <param name="size">Size per page.</param>
		/// <param name="includeProperties">Included properties.</param>
		public Task<List<T>> GetManyAsync(
			Expression<Func<T, bool>> filter,
			out int total,
			int index,
			int size,
			params Expression<Func<T, object>>[] includeProperties)
		{
			// ***********************
			// Set included properties
			// ***********************
			IQueryable<T> query = _dbSet;
			GetPropertyNames(ref query, includeProperties);

			if (filter != null)
			{
				query = query.Where(filter);
			}

			total = query.Count();

			return query.Skip(size * index).Take(size).ToListAsync();
		}

		/// <summary>
		/// Get multiple records from the database.
		/// </summary>
		/// <param name="filter">Filter that describeds the records to look for.</param>
		/// <param name="includeProperties">Included properties.</param>
		public Task<List<T>> GetManyAsync(
			Expression<Func<T, bool>> filter = null,
			params Expression<Func<T, object>>[] includeProperties)
		{
			// ***********************
			// Set included properties
			// ***********************
			IQueryable<T> query = _dbSet;
			GetPropertyNames(ref query, includeProperties);

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.ToListAsync();
		}

		#endregion

		#region Create

		/// <summary>
		/// Create a record.
		/// </summary>
		/// <param name="entity">Record to add into the database.</param>
		/// <returns></returns>
		public T Create(T entity)
		{
			var newEntry = _dbSet.Add(entity);
			return newEntry;
		}

		#endregion

		#region Update

		/// <summary>
		/// Update a record on the database.
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		public void Update(T entity)
		{
			var entry = _dbContext.Entry(entity);
			var key = GetPrimaryKey(entry);

			if (entry.State != EntityState.Detached) return;

			var currentEntry = _dbSet.Find(key);
			if (currentEntry != null)
			{
				var attachedEntry = _dbContext.Entry(currentEntry);
				attachedEntry.CurrentValues.SetValues(entity);
			}
			else
			{
				_dbSet.Attach(entity);
				entry.State = EntityState.Modified;
			}
		}

		#endregion

		#region Delete

		/// <summary>
		/// Delete an item from the table.
		/// </summary>
		/// <param name="id">Id of the record.</param>
		public void Delete(int id)
		{
			var entityToDelete = _dbSet.Find(id);
			Delete(entityToDelete);
		}

		/// <summary>
		/// Delete a record from the database.
		/// </summary>
		/// <param name="entity">Instance of the record</param>
		public void Delete(T entity)
		{
			if (_dbContext.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
		}

		/// <summary>
		/// Delete the record(s) that is being described on the filter parameter.
		/// </summary>
		/// <param name="filter">Record description filter.</param>
		public void Delete(Expression<Func<T, bool>> filter)
		{
			var entitiesToDelete = GetMany(filter);

			foreach (var entity in entitiesToDelete.Where(entity => _dbContext.Entry(entity).State == EntityState.Detached))
			{
				_dbSet.Attach(entity);
			}

			_dbSet.RemoveRange(entitiesToDelete);
		}

		#endregion

		#region Misc

		/// <summary>
		/// Get all items from the table.
		/// </summary>
		public List<T> All()
		{
			return _dbSet.ToList();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Include the property names into the query (JOIN)
		/// </summary>
		/// <param name="query">Current query.</param>
		/// <param name="includedProperties">Properties to include into the query.</param>
		/// <returns></returns>
		private IQueryable<T> GetPropertyNames(ref IQueryable<T> query, params Expression<Func<T, object>>[] includedProperties)
		{
			if (includedProperties == null) return query;

			foreach (var includedProperty in includedProperties)
			{
				var memberExpr = includedProperty.Body as MemberExpression;
				query = _dbSet.Include(memberExpr.Member.Name);
			}

			return query;
		}

		/// <summary>
		/// Get the primary key of an entity.
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		private static object GetPrimaryKey(DbEntityEntry entry)
		{
			var entity = entry.Entity;
			var property =
				entity.GetType()
					.GetProperties()
					.FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));

			return property?.GetValue(entity, null);
		}

		#endregion

		#region IDisposable

		/// <summary>
		/// Dispose handler.
		/// </summary>
		public void Dispose()
		{
			_dbSet = null;
			_dbContext.Dispose();
		}

		#endregion
	}
}
