// *****************************
// Header initializer
// *****************************

Application.prototype.header = function () {
	/// <summary>Header initializer.</summary>

	// private members
	var appOpts = null;

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOptions">Application option object.</param>

		// set the application options member...
		appOpts = appOptions;
		appOpts.log('setting up header...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------

		// -------------------------
		// Heading elements toggler
		// -------------------------

		// Add control button toggler to page and panel headers if have heading elements
		$('.panel-heading, .page-header-content, .panel-body, .panel-footer')
			.has('> .heading-elements')
			.append('<a class="heading-elements-toggle"><i class="icon-more"></i></a>');

		// Toggle visible state of heading elements
		$('.heading-elements-toggle').on('click', function () {
			$(this).parent().children('.heading-elements')
				.toggleClass('visible');
		});

		// -------------------------
		// Breadcrumb elements toggler
		// -------------------------

		// Add control button toggler to breadcrumbs if has elements
		$('.breadcrumb-line').has('.breadcrumb-elements')
			.append('<a class="breadcrumb-elements-toggle"><i class="icon-menu-open"></i></a>');

		// Toggle visible state of breadcrumb elements
		$('.breadcrumb-elements-toggle').on('click', function () {
			$(this).parent().children('.breadcrumb-elements')
				.toggleClass('visible');
		});
	};

	return {
		init : init
	};

}();