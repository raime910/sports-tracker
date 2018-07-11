using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace StatTrack.WEB.Plumbing.Extension
{
	public static class FormsExt
	{
		/// <summary>
		/// Converts the collection into a select list.
		/// </summary>
		/// <typeparam name="T">Type of the current collection.</typeparam>
		/// <param name="collection">Collection instance.</param>
		/// <param name="value">Value expression.</param>
		/// <param name="text">Text expression.</param>
		/// <param name="selectedValue">Optional selected value.</param>
		/// <returns></returns>
		public static SelectList ToSelectList<T>(
			this IEnumerable<T> collection,
			Expression<Func<T, object>> value,
			Expression<Func<T, object>> text,
			int selectedValue = -1)
		{
			var valuePropName = GetName(value);
			var textPropName = GetName(text);

			return new SelectList(collection, valuePropName, textPropName, selectedValue);
		}

		/// <summary>
		/// Converts the collection into a select list.
		/// </summary>
		/// <typeparam name="T">Type of the current collection.</typeparam>
		/// <param name="collection">Collection instance.</param>
		/// <param name="value">Value expression.</param>
		/// <param name="text">Text expression.</param>
		/// <param name="group">Group expression.</param>
		/// <param name="selectedValue">Optional selected value.</param>
		public static SelectList ToSelectList<T>(
			this IEnumerable<T> collection,
			Expression<Func<T, object>> value,
			Expression<Func<T, object>> text,
			Expression<Func<T, object>> group,
			int selectedValue = -1)
		{
			var valuePropName = GetName(value);
			var textPropName = GetName(text);
			var groupPropName = GetName(group);

			return new SelectList(collection, valuePropName, textPropName, groupPropName, selectedValue);
		}

		/// <summary>
		/// Get the propert name from the expression.
		/// </summary>
		/// <typeparam name="T">Type of the current collection.</typeparam>
		/// <param name="exp">Expression to evaluate.</param>
		private static string GetName<T>(Expression<Func<T, object>> exp)
		{
			var body = exp.Body as MemberExpression;

			// if body is not a member expression, try as a unary expression...
			if (body == null)
			{
				var ubody = (UnaryExpression)exp.Body;
				body = ubody.Operand as MemberExpression;
			}

			// if body is a unary expression, this should be true.
			if (body != null)
			{
				return body.Member.Name;
			}

			// else fail...
			throw new Exception("Failed to find expression member name.");
		}
	}
}