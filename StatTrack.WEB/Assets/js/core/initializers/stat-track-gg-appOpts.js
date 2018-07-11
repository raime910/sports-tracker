// *****************************
// Application options
// *****************************

var ApplicationOptions = function(appName) {
	/// <summary>Create a new instance of the application option object.</summary>
	/// <param name="appName">Application name.</param>

	// setup globals here...
	this.APP_NAME = appName; // Application name
	this.IS_DEBUG = true; // Debug flag
};

ApplicationOptions.prototype = function () {
	/// <summary>Main application initializer</summary>

	var logEnabled = true;

	var enableLog = function (isEnabled) {
		/// <summary>Enable or disable logging for the application.</summary>
		/// <param name="isEnabled">True to enable logging or false to disable it.</param>
		logEnabled = isEnabled;
	};

	var log = function (msg) {
		/// <summary>Log information for debug purposes. Messages are displayed on the console.</summary>
		/// <param name="msg">Message to print into the console.</param>
		if (logEnabled) {
			console.debug(this.APP_NAME + ' > ' + msg);
		}
	};

	var debugCheck = function () {
		/// <summary>Checks to see if debug mode is enabled or not.</summary>
		if (this.IS_DEBUG) {
			this.log('application is in DEBUG mode.');
		} else {
			this.log('application is in RELEASE mode.');
		}
	};

	return {
		// globals
		APP_NAME: this.APP_NAME,
		IS_DEBUG: this.IS_DEBUG,
		// methods
		log: log,
		enableLog: enableLog,
		debugCheck: debugCheck
	};

}();