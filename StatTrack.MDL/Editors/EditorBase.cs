using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StatTrack.MDL.Editors
{
	public class EditorBase : IValidatableObject
	{
		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			throw new NotImplementedException();
		}
	}
}
