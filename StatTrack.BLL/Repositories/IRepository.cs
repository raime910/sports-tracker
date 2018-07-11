using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StatTrack.BLL.Repositories
{
	public interface IRepository<T> : IDisposable where T : class
	{
		bool Any(Expression<Func<T, bool>> filter);

		Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

		int Count();

		int Count(Expression<Func<T, bool>> filter);

		Task<int> CountAsync();

		Task<int> CountAsync(Expression<Func<T, bool>> filter);

		T GetOne(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includedProperties);

		Task<T> GetOneAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includedProperties);

		List<T> GetMany(
			Expression<Func<T, bool>> filter,
			out int total,
			int index,
			int size,
			params Expression<Func<T, object>>[] includedProperties);

		List<T> GetMany(
			Expression<Func<T, bool>> filter = null,
			params Expression<Func<T, object>>[] includedProperties);

		Task<List<T>> GetManyAsync(
			Expression<Func<T, bool>> filter,
			out int total,
			int index,
			int size,
			params Expression<Func<T, object>>[] includedProperties);

		Task<List<T>> GetManyAsync(
			Expression<Func<T, bool>> filter = null,
			params Expression<Func<T, object>>[] includedProperties);

		T Create(T entity);

		void Update(T entity);

		void Delete(int id);

		void Delete(T entity);

		void Delete(Expression<Func<T, bool>> filter);

		List<T> All();
	}
}
