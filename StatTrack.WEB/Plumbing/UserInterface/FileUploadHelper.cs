using StatTrack.WEB.Plumbing.Extension;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace StatTrack.WEB.Plumbing.UserInterface
{
	public static class FileUploadHelper
	{
		/// <summary>
		/// Builds error message summary display for the model.
		/// </summary>
		/// <typeparam name="TModel">Model instance type.</typeparam>
		/// <param name="helper">Html helper instance.</param>
		/// <param name="headerText">Header text to display.</param>
		public static MvcHtmlString StggFileUpload<TModel>(this HtmlHelper<TModel> helper, string headerText = "")
		{
			var output = string.Empty;

			if (!helper.ViewData.ModelState.IsValid)
			{
				// Build the header if needed
				var header = string.Empty;

				if (!string.IsNullOrEmpty(headerText))
				{
					header = $"<div class='panel-heading'><h6 class='panel-title'>{headerText}</h6></div>";
				}

				// Get model summary error messages.
				if (helper.ViewData.ModelState.Any(x => x.Key == ValidationExt.MODEL_SUMMARY_ERROR_KEY || string.IsNullOrEmpty(x.Key)))
				{
					// Build the display for the error messages...
					var content = new StringBuilder();
					ModelErrorCollection errors;

					// Summary errors...
					if (helper.ViewData.ModelState[ValidationExt.MODEL_SUMMARY_ERROR_KEY] != null)
					{
						errors = helper.ViewData.ModelState[ValidationExt.MODEL_SUMMARY_ERROR_KEY].Errors;
						foreach (var err in errors)
						{
							content.Append($"<span class='help-block text-danger'><i class='icon-cancel-circle2 position-left'></i>{err.ErrorMessage}</span>");
						}
					}

					// IValidatableObject implementation errors...
					if (helper.ViewData.ModelState[ValidationExt.MODEL_VALIDATABLE_OBJ_ERROR_KEY] != null)
					{
						errors = helper.ViewData.ModelState[ValidationExt.MODEL_VALIDATABLE_OBJ_ERROR_KEY].Errors;
						foreach (var err in errors)
						{
							content.Append($"<span class='help-block text-danger'><i class='icon-cancel-circle2 position-left'></i>{err.ErrorMessage}</span>");
						}
					}

					output = $"<div class='panel panel-flat border-left-danger'>{header}<div class='panel-body'>{content}</div></div>";
				}
			}

			return new MvcHtmlString(output);
		}
	}
}