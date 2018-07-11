/// <reference path="stat-track-gg-alert.js" />
/// <reference path="stat-track-gg-appOpts.js" />
/// <reference path="stat-track-gg-datetime.js" />
/// <reference path="stat-track-gg-form.js" />
/// <reference path="stat-track-gg-header.js" />
/// <reference path="stat-track-gg-input.js" />
/// <reference path="stat-track-gg-navigation.js" />
/// <reference path="stat-track-gg-plugins.js" />
/// <reference path="stat-track-gg-select.js" />

$(function () {
	var appOpts = new ApplicationOptions('StatTrack.gg');
	var app = new Application(appOpts).init();
});

// *****************************
// Application
// *****************************

var Application = function (appOpts) {
	// setup globals here...
	this.APP_OPTS = appOpts;
};

Application.prototype = function () {
	/// <summary>Application UI initializer.</summary>
	var app;

	var disableCssTransitionsOnPageLoad = function () {
		app.APP_OPTS.log('disabling CSS transitions on page load...');
		$('body').addClass('no-transitions');
	};

	var init = function () {
		/// <summary>Main initializer.</summary>
		app = this;

		// initialize the application
		app.APP_OPTS.log('application initializer is now starting...');

		disableCssTransitionsOnPageLoad();

		app.header.init(this.APP_OPTS);
		app.navigation.init(this.APP_OPTS);
		app.input.init(this.APP_OPTS);
		app.select.init(this.APP_OPTS);
		app.plugins.init(this.APP_OPTS);
		app.datetime.init(this.APP_OPTS);
		app.form.init(this.APP_OPTS);
		app.alert.init(this.APP_OPTS);

		return app;
	};

	return {
		init: init
	};

}();
