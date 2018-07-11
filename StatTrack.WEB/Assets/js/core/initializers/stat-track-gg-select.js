// *****************************
// Select initializer
// *****************************

Application.prototype.select = function () {
	/// <summary>Dropdown intializer for the application.</summary>

	// private members
	var appOpts = null;

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOptions">Application option object.</param>

		appOpts = appOptions;
		appOpts.log('setting up dropdownlists...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------
		$('.select2').select2({
			allowClear: true,
			minimumResultsForSearch: -1,
			placeholder: function () {
				$(this).data('placeholder');
			}
		});

		$('.select2-multiple').attr('multiple', 'multiple');

		$('.select2-multiple').select2({
			minimumResultsForSearch: -1,
			placeholder: function () {
				$(this).data('placeholder');
			}
		});
	};

	return {
		init : init
	};

}();