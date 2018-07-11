// *****************************
// Textbox initializer
// *****************************

Application.prototype.form = function () {
	/// <summary>Form initializer.</summary>

	// private members
	var appOpts = null;

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOpts">Application option object.</param>

		appOpts = appOptions;
		appOpts.log('setting up form...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------
	};



	return {
		init : init
	};

}();