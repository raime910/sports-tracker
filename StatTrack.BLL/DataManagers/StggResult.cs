using System.Collections.Generic;
using System.Linq;

namespace StatTrack.BLL.DataManagers
{
	public class StggResult<TStatus, TValue>
	{

		#region Ctor

		internal StggResult(TStatus status, TValue value)
		{
			Status = status;
			Value = value;
			_errors = new List<string>();
		}

		internal StggResult()
		{
			_errors = new List<string>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Status or outcome of the request.
		/// </summary>
		public TStatus Status { get; protected set; }

		/// <summary>
		/// Output of the request.
		/// </summary>
		public TValue Value { get; protected set; }

		/// <summary>
		/// Collection of error messages.
		/// </summary>
		public IEnumerable<string> Errors => _errors;

		private readonly List<string> _errors;

		/// <summary>
		/// Returns true if there is an error message.
		/// </summary>
		public bool HasError => _errors.Any();

		#endregion

		#region Methods

		/// <summary>
		/// Add error message.
		/// </summary>
		/// <param name="errorMsg">Error message.</param>
		public void AddError(string errorMsg)
		{
			_errors.Add(errorMsg);
		}

		/// <summary>
		/// Add error message.
		/// </summary>
		/// <param name="errorMsgs">Error messages.</param>
		public void AddErrors(IEnumerable<string> errorMsgs)
		{
			_errors.AddRange(errorMsgs);
		}

		/// <summary>
		/// Set the Output property.
		/// </summary>
		/// <param name="value">Output instance.</param>
		public void SetValue(TValue value)
		{
			Value = value;
		}

		/// <summary>
		/// Set the Output property.
		/// </summary>
		/// <param name="status">Operation status.</param>
		public void SetStatus(TStatus status)
		{
			Status = status;
		}

		/// <summary>
		/// Set the Output property.
		/// </summary>
		/// <param name="status">Status of the operation.</param>
		/// <param name="value">Output instance.</param>
		public void SetValue(TStatus status, TValue value)
		{
			Value = value;
			Status = status;
		}

		#endregion
	}

	#region Derived StggResult and status enum
	public class StggResult<TResult> : StggResult<StggResultStatus, TResult>
	{
		internal StggResult(TResult value)
			: base(StggResultStatus.Succeeded, value)
		{
			Value = value;
		}

		public StggResult() { }

		public new void AddError(string errorMsg)
		{
			base.AddError(errorMsg);
			SetStatus(StggResultStatus.Failed);
		}

		public new void AddErrors(IEnumerable<string> errorMsgs)
		{
			base.AddErrors(errorMsgs);
			SetStatus(StggResultStatus.Failed);
		}
	}

	public class StggResult : StggResult<bool>
	{
		public StggResult()
		{
		}

		internal StggResult(bool value)
			: base(value)
		{
			Value = value;
		}
	}

	public enum StggResultStatus
	{
		Succeeded,
		Failed
	}
	#endregion
}
