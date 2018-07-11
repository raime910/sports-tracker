using System;
using System.Web;

namespace StatTrack.WEB
{
	internal static class SessionManager
	{
		#region Public methods

		/// <summary>
		/// Get a value from the session state.
		/// </summary>
		/// <typeparam name="T">Return type.</typeparam>
		/// <param name="key">Key of the session state variable.</param>
		internal static T Get<T>(SessionKey key)
		{
			var result = default(T);

			if (Exists(key))
			{
				result = (T)Convert.ChangeType(HttpContext.Current.Session[key.Name], typeof(T));
			}

			return result;
		}

		internal static bool Exists(SessionKey key)
		{
			try
			{
				return HttpContext.Current.Session[key.Name] != null;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Stores a value into the session state.
		/// </summary>
		/// <param name="key">Key of the session state variable.</param>
		/// <param name="value">Value to store</param>
		internal static void Set(SessionKey key, dynamic value)
		{
			HttpContext.Current.Session.Add(key.Name, value);
		}

		/// <summary>
		/// Removes a session variable from.
		/// </summary>
		internal static void Abandon()
		{
			HttpContext.Current.Session.Abandon();
		}

		#endregion

	}
}