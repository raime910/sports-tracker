// *****************************
// Plugins initializer
// *****************************

Application.prototype.plugins = function () {
	/// <summary>Plugins intializer for the application.</summary>

	// private members
	var appOpts = null;

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOptions">Application option object.</param>

		appOpts = appOptions;
		appOpts.log('setting up plugins...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------

		// ----------------------------------------
		// Popover
		// ----------------------------------------
		$('[data-popup="popover"]').popover();

		// ----------------------------------------
		// Tooltip
		// ----------------------------------------
		$('[data-popup="tooltip"]').tooltip();
	};

	return {
		init : init
	};

}();
