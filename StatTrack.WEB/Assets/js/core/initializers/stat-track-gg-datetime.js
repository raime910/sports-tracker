// *****************************
// Textbox initializer
// *****************************

Application.prototype.datetime = function () {
	/// <summary>Textbox initializer.</summary>

	// private members
	var appOpts = null;

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOpts">Application option object.</param>

		appOpts = appOptions;
		appOpts.log('setting up datetime picker using [anytime] plugin...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------
		// Basic usage
		$('.anytime-date').AnyTime_picker({
			format: '%W, %M %D in the Year %z %E',
			firstDOW: 1
		});
		
		// Time picker
		$('.anytime-time').AnyTime_picker({
			format: '%H:%i'
		});
		
		// Display hours only
		$('.anytime-time-hours').AnyTime_picker({
			format: '%l %p'
		});
		
		// Date and time
		$('.anytime-both').AnyTime_picker({
			format: '%M %D %H:%i'
		});
		
		// Custom display format
		$('.anytime-weekday').AnyTime_picker({
			format: '%W, %D of %M, %Z'
		});
		
		// Numeric date
		$('.anytime-month-numeric').AnyTime_picker({
			format: '%m/%d/%Z'
		});
		
		// Month and day
		$('.anytime-month-day').AnyTime_picker({
			format: '%D of %M'
		});

	};

	return {
		init : init
	};

}();