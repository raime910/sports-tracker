using StatTrack.BLL.DataManagers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StatTrack.WEB.Plumbing.Extension
{
	public static class ValidationExt
	{
		public const string MODEL_SUMMARY_ERROR_KEY = "MODEL_SUMMARY_ERROR_KEY";
		public const string MODEL_VALIDATABLE_OBJ_ERROR_KEY = "";

		public static void AddModelSummaryError(this ModelStateDictionary modelStatesStateDictionary, string message)
		{
			modelStatesStateDictionary.AddModelError(MODEL_SUMMARY_ERROR_KEY, message);
		}

		public static void AddModelSummaryError(this ModelStateDictionary modelStatesStateDictionary, IEnumerable<string> messages)
		{
			foreach (var errorMessage in messages)
			{
				modelStatesStateDictionary.AddModelError(MODEL_SUMMARY_ERROR_KEY, errorMessage);
			}
		}

		public static void AddModelSummaryError(this ModelStateDictionary modelStatesStateDictionary, StggResult stggResult)
		{
			foreach (var errorMessage in stggResult.Errors)
			{
				modelStatesStateDictionary.AddModelError(MODEL_SUMMARY_ERROR_KEY, errorMessage);
			}
		}
	}
}