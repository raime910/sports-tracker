using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	public class StggViewModelBase : IValidatableObject
	{
		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			throw new NotImplementedException();
		}
	}
}
