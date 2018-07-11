// *****************************
// Textbox initializer
// *****************************

Application.prototype.input = function () {
	/// <summary>Textbox initializer.</summary>

	// private members
	var appOpts = null;

	var setupFileUploader = function() {
		appOpts.log('setting up file-uploader...');

		$('.file-styled').uniform({
			fileButtonClass: 'action btn bg-warning-400'
		});
	};

	var setupCropper = function () {
		appOpts.log('setting up file-cropper...');

		$('body').on('change', '.cropper-input', function () {
			var input = this;
			var cropperSelector = $(this).data('img-cropper');
			var cropper = $(cropperSelector + ' img');
			var fileReader = new FileReader();

			fileReader.readAsDataURL(this.files[0]);
			fileReader.onload = function () {
				// Destroy cropper
				$(cropper).cropper('destroy');

				// Replace url
				$(cropper).attr('src', this.result);

				// Start cropper
				$(cropper).cropper({
					cropBoxResizable: false,
					data: {
						x: 10,
						y: 10,
						width: 300,
						height: 300
					},
					cropend: function (e) {
						$(input).val(e.x + ', ' + e.y + ',' + e.width + ',' + e.height);
					}
				});
			}
		});
	};

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOpts">Application option object.</param>

		appOpts = appOptions;
		appOpts.log('setting up input elements...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------

		setupFileUploader();
		setupCropper();
	};

	return {
		init : init
	};

}();