using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace StatTrack.WEB
{
	internal static class CacheManager
	{
		#region Create

		/// <summary>
		/// Add a new cache value.
		/// </summary>
		/// <param name="name">Key</param>
		/// <param name="value">Value</param>
		/// <param name="overwrite">Overwrite Cache</param>
		internal static void Add(string name, object value, bool overwrite = true)
		{
			Add(name, value, DateTime.Now.AddMinutes(30), overwrite);
		}

		/// <summary>
		/// Create a cache value with dependency
		/// </summary>
		/// <param name="name">Unique key or name for the cache item.</param>
		/// <param name="value">Value to store into the cache.</param>
		/// <param name="overwrite">True to overwrite existing cache.</param>
		/// <param name="expiration">Expiration date for this cache value.</param>
		/// <param name="dependencies">Dependency of this cache item.</param>
		/// <param name="cacheItemRemovedCallback">A delegate that is called when this cache value is removed.</param>
		internal static void Add(
			string name,
			object value,
			DateTime expiration,
			bool overwrite,
			CacheDependency dependencies = null,
			CacheItemRemovedCallback cacheItemRemovedCallback = null)
		{
			// Check if the cache is already in the server.
			var oCache = HttpContext.Current.Cache[name];

			// If the cache already exists
			// Check if the user wants to remove the existing cache.
			if (oCache != null && overwrite)
				HttpContext.Current.Cache.Remove(name);

			HttpContext.Current.Cache.Add(
				name,
				value,
				dependencies,
				expiration,
				Cache.NoSlidingExpiration,
				CacheItemPriority.Default,
				cacheItemRemovedCallback
				);
		}

		#endregion

		#region Get

		/// <summary>
		/// Returns the cache value if it exists get the value from the delegate and return.
		/// </summary>
		/// <typeparam name="T">Expected return type.</typeparam>
		/// <param name="name">Name of the cache object.</param>
		/// <param name="task">Task that can create the value if it does not exist.</param>
		/// <param name="expiration">Expiration date of the cache object.</param>
		internal static object GetIfExist<T>(string name, Func<object> task, DateTime expiration)
		{
			if (Exists(name))
			{
				return Get<T>(name);
			}

			// Get the value from the task...
			var value = task();

			// Add it into the cache.
			Add(name, value);

			// return the value to the caller.
			return value;
		}

		/// <summary>
		/// Gets the cache to the server.
		/// </summary>
		/// <param name="name">Key</param>
		/// <returns>object</returns>
		internal static T Get<T>(string name)
		{
			return (T)HttpContext.Current.Cache.Get(name);
		}

		#endregion

		#region Remove

		/// <summary>
		/// Removes all the cache from the server.
		/// </summary>
		internal static void Remove()
		{
			var oContext = HttpContext.Current;
			foreach (DictionaryEntry cache in oContext.Cache)
			{
				RemoveSpecificItem(cache.Key.ToString());
			}
		}

		/// <summary>
		/// Removes all the cache from the server with filtering.
		/// </summary>
		/// <param name="name">Filter</param>
		internal static void RemoveMatch(string name)
		{
			var oContext = HttpContext.Current;

			foreach (var cache in oContext.Cache.Cast<DictionaryEntry>()
				.Where(cache => cache.Key.ToString().Contains(name)))
			{
				RemoveSpecificItem(cache.Key.ToString());
			}
		}

		/// <summary>
		/// Removes a specific cache from the server.
		/// </summary>
		/// <param name="name">Key</param>
		internal static void RemoveSpecificItem(string name)
		{
			HttpContext.Current.Cache.Remove(name);
		}

		#endregion

		#region Check

		/// <summary>
		/// Check if the cache already exists on the server.
		/// </summary>
		/// <param name="name">Key</param>
		/// <returns>bool</returns>
		internal static bool Exists(string name)
		{
			var bExist = false;

			var oCache = Get<object>(name);

			if (oCache != null)
			{
				bExist = true;
			}

			return bExist;
		}

		#endregion
	}
}