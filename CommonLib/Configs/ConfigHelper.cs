using System;
using System.Configuration;
using System.Xml;

namespace CommonLib.Configs
{
	public static class ConfigHelper
	{
		/// <summary>
		/// Get the value of a application setting.
		/// </summary>
		/// <typeparam name="T">Output type.</typeparam>
		/// <param name="key">Key of the application setting.</param>
		public static T Get<T>(string key)
		{
			return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
		}

		/// <summary>
		/// Set the value of an application setting.
		/// </summary>
		/// <param name="key">Key of the application setting.</param>
		/// <param name="value">New value to change the setting to.</param>
		public static void Set(string key, string value)
		{
			var xmlDoc = new XmlDocument();

			xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

			var selectSingleNode = xmlDoc.DocumentElement?.FirstChild.SelectSingleNode("descendant::" + key);

			if (selectSingleNode?.Attributes != null)
			{
				selectSingleNode.Attributes[0].Value = value;
			}

			xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

			ConfigurationManager.RefreshSection("section/subSection");
		}
	}
}
