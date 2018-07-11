// *****************************
// Navigation initializer
// *****************************

Application.prototype.navigation = function () {
	/// <summary>Navigation initializer.</summary>

	// private members
	var appOpts = null;

	var setupMainNavigation = function () {
		appOpts.log('executing setupMainNavigation()...');

		// -------------------------
		// Main navigation
		// -------------------------

		// Add 'active' class to parent list item in all levels
		$('.navigation').find('li.active')
			.parents('li')
			.addClass('active');

		// Hide all nested lists
		$('.navigation').find('li')
			.not('.active, .category-title')
			.has('ul').children('ul').addClass('hidden-ul');

		// Highlight children links
		$('.navigation').find('li')
			.has('ul')
			.children('a')
			.addClass('has-ul');

		// Add active state to all dropdown parent levels
		$('.dropdown-menu:not(.dropdown-content), .dropdown-menu:not(.dropdown-content) .dropdown-submenu')
			.has('li.active')
			.addClass('active')
			.parents('.navbar-nav .dropdown:not(.language-switch), .navbar-nav .dropup:not(.language-switch)')
			.addClass('active');

		// Main navigation tooltips positioning
		// -------------------------

		// Left sidebar
		$('.navigation-main > .navigation-header > i').tooltip({
			placement: 'right',
			container: 'body'
		});

		// Collapsible functionality
		// -------------------------

		// Main navigation
		$('.navigation-main').find('li').has('ul').children('a').on('click', function(e) {
			e.preventDefault();

			// Collapsible
			$(this).parent('li').not('.disabled').not($('.sidebar-xs').not('.sidebar-xs-indicator').find('.navigation-main').children('li')).toggleClass('active').children('ul').slideToggle(250);

			// Accordion
			if ($('.navigation-main').hasClass('navigation-accordion')) {
				$(this).parent('li').not('.disabled').not($('.sidebar-xs').not('.sidebar-xs-indicator').find('.navigation-main').children('li')).siblings(':has(.has-ul)').removeClass('active').children('ul').slideUp(250);
			}
		});

		// Alternate navigation
		$('.navigation-alt').find('li').has('ul').children('a').on('click', function(e) {
			e.preventDefault();

			// Collapsible
			$(this).parent('li').not('.disabled').toggleClass('active').children('ul').slideToggle(200);

			// Accordion
			if ($('.navigation-alt').hasClass('navigation-accordion')) {
				$(this).parent('li').not('.disabled').siblings(':has(.has-ul)').removeClass('active').children('ul').slideUp(200);
			}
		});
	};

	var calculateContainerHeight = function () {
		appOpts.log('executing calculateContainerHeight()...');
		var availableHeight = $(window).height() - $('.page-container').offset().top - $('.navbar-fixed-bottom').outerHeight();
		$('.page-container').attr('style', 'min-height:' + availableHeight + 'px');
	};

	var setupNavbar = function () {
		appOpts.log('executing setupNavbar()...');

		// Prevent dropdown from closing on click
		$(document).on('click', '.dropdown-content', function (e) {
			e.stopPropagation();
		});

		// Disabled links
		$('.navbar-nav .disabled a').on('click', function (e) {
			e.preventDefault();
			e.stopPropagation();
		});

		// Show tabs inside dropdowns
		$('.dropdown-content a[data-toggle="tab"]').on('click', function (e) {
			$(this).tab('show');
		});
	};

	var setupSidebar = function () {
		appOpts.log('executing setupSidebar()...');

		// This is the event handler for when the user hovers on the mini side bar.
		function miniSidebar() {
			$('.sidebar-main.sidebar-fixed .sidebar-content').on('mouseenter', function () {
				if ($('body').hasClass('sidebar-xs')) {
					// Expand fixed navbar
					$('body').removeClass('sidebar-xs').addClass('sidebar-fixed-expanded');
				}
			}).on('mouseleave', function () {
				if ($('body').hasClass('sidebar-fixed-expanded')) {
					// Collapse fixed navbar
					$('body').removeClass('sidebar-fixed-expanded').addClass('sidebar-xs');
				}
			});
		}

		// Initialize
		miniSidebar();

		// Toggle mini sidebar
		$('.sidebar-main-toggle').on('click', function (e) {
			// Initialize mini sidebar 
			miniSidebar();
		});

		// ------------------------------
		// Nice scroll
		// ------------------------------

		// Setup
		function initScroll() {
			$(".sidebar-fixed .sidebar-content").niceScroll({
				mousescrollstep: 100,
				cursorcolor: '#ccc',
				cursorborder: '',
				cursorwidth: 3,
				hidecursordelay: 100,
				autohidemode: 'scroll',
				horizrailenabled: false,
				preservenativescrolling: false,
				railpadding: {
					right: 0.5,
					top: 1.5,
					bottom: 1.5
				}
			});
		}

		// Remove
		function removeScroll() {
			$(".sidebar-fixed .sidebar-content").getNiceScroll().remove();
			$(".sidebar-fixed .sidebar-content").removeAttr('style').removeAttr('tabindex');
		}

		// Initialize
		initScroll();

		// Remove scrollbar on mobile
		$(window).on('resize', function () {
			setTimeout(function () {
				if ($(window).width() <= 768) {

					// Remove nicescroll on mobiles
					removeScroll();
				}
				else {

					// Init scrollbar
					initScroll();
				}
			}, 100);
		}).resize();
	};

	var setupSidebarCategories = function () {
		appOpts.log('executing setupSidebarCategories()...');

		// -------------------------
		// Panels
		// -------------------------
		$('.panel [data-action=reload]').click(function (e) {
			e.preventDefault();
			var block = $(this).parent().parent().parent().parent().parent();
			$(block).block({
				message: '<i class="icon-spinner2 spinner"></i>',
				overlayCSS: {
					backgroundColor: '#fff',
					opacity: 0.8,
					cursor: 'wait',
					'box-shadow': '0 0 0 1px #ddd'
				},
				css: {
					border: 0,
					padding: 0,
					backgroundColor: 'none'
				}
			});

			// For demo purposes
			window.setTimeout(function () {
				$(block).unblock();
			}, 2000);
		});

		// -------------------------
		// Sidebar categories
		// -------------------------

		$('.category-title [data-action=reload]').click(function (e) {
			e.preventDefault();
			var block = $(this).parent().parent().parent().parent();
			$(block).block({
				message: '<i class="icon-spinner2 spinner"></i>',
				overlayCSS: {
					backgroundColor: '#000',
					opacity: 0.5,
					cursor: 'wait',
					'box-shadow': '0 0 0 1px #000'
				},
				css: {
					border: 0,
					padding: 0,
					backgroundColor: 'none',
					color: '#fff'
				}
			});

			// For demo purposes
			window.setTimeout(function () {
				$(block).unblock();
			}, 2000);
		});
		
		// -------------------------
		// Light sidebar categories
		// -------------------------

		$('.sidebar-default .category-title [data-action=reload]').click(function (e) {
			e.preventDefault();
			var block = $(this).parent().parent().parent().parent();
			$(block).block({
				message: '<i class="icon-spinner2 spinner"></i>',
				overlayCSS: {
					backgroundColor: '#fff',
					opacity: 0.8,
					cursor: 'wait',
					'box-shadow': '0 0 0 1px #ddd'
				},
				css: {
					border: 0,
					padding: 0,
					backgroundColor: 'none'
				}
			});

			// For demo purposes
			window.setTimeout(function () {
				$(block).unblock();
			}, 2000);
		});

		// -------------------------
		// Collapse elements
		// -------------------------

		// -------------------------
		// Sidebar categories
		// -------------------------

		// Hide if collapsed by default
		$('.category-collapsed').children('.category-content').hide();

		// Rotate icon if collapsed by default
		$('.category-collapsed').find('[data-action=collapse]').addClass('rotate-180');

		// Collapse on click
		$('.category-title [data-action=collapse]').click(function (e) {
			e.preventDefault();
			var $categoryCollapse = $(this).parent().parent().parent().nextAll();

			$(this).parents('.category-title').toggleClass('category-collapsed');
			$(this).toggleClass('rotate-180');

			calculateContainerHeight(); // adjust page height
			$categoryCollapse.slideToggle(150);
		});

		// -------------------------
		// Panels
		// -------------------------

		// Hide if collapsed by default
		$('.panel-collapsed').children('.panel-heading').nextAll().hide();

		// Rotate icon if collapsed by default
		$('.panel-collapsed').find('[data-action=collapse]').addClass('rotate-180');


		// Collapse on click
		$('.panel [data-action=collapse]').click(function (e) {
			e.preventDefault();
			var $panelCollapse = $(this).parent().parent().parent().parent().nextAll();
			$(this).parents('.panel').toggleClass('panel-collapsed');
			$(this).toggleClass('rotate-180');

			calculateContainerHeight(); // recalculate page height

			$panelCollapse.slideToggle(150);
		});

		// Remove elements
		// -------------------------

		// Panels
		$('.panel [data-action=close]').click(function (e) {
			e.preventDefault();
			var $panelClose = $(this).parent().parent().parent().parent().parent();

			calculateContainerHeight(); // recalculate page height

			$panelClose.slideUp(150, function () {
				$(this).remove();
			});
		});


		// Sidebar categories
		$('.category-title [data-action=close]').click(function (e) {
			e.preventDefault();
			var $categoryClose = $(this).parent().parent().parent().parent();

			calculateContainerHeight(); // recalculate page height

			$categoryClose.slideUp(150, function () {
				$(this).remove();
			});
		});
	};

	var setupOppositeSidebar = function () {
		appOpts.log('executing setupOppositeSidebar()...');

		// -------------------------
		// Opposite sidebar
		// -------------------------

		// Collapse main sidebar if opposite sidebar is visible
		$(document).on('click', '.sidebar-opposite-toggle', function (e) {
			e.preventDefault();

			// Opposite sidebar visibility
			$('body').toggleClass('sidebar-opposite-visible');

			// If visible
			if ($('body').hasClass('sidebar-opposite-visible')) {

				// Make main sidebar mini
				$('body').addClass('sidebar-xs');

				// Hide children lists
				$('.navigation-main').children('li').children('ul').css('display', '');
			}
			else {

				// Make main sidebar default
				$('body').removeClass('sidebar-xs');
			}
		});

		// Hide main sidebar if opposite sidebar is shown
		$(document).on('click', '.sidebar-opposite-main-hide', function (e) {
			e.preventDefault();

			// Opposite sidebar visibility
			$('body').toggleClass('sidebar-opposite-visible');

			// If visible
			if ($('body').hasClass('sidebar-opposite-visible')) {

				// Hide main sidebar
				$('body').addClass('sidebar-main-hidden');
			}
			else {

				// Show main sidebar
				$('body').removeClass('sidebar-main-hidden');
			}
		});

		// Hide secondary sidebar if opposite sidebar is shown
		$(document).on('click', '.sidebar-opposite-secondary-hide', function (e) {
			e.preventDefault();

			// Opposite sidebar visibility
			$('body').toggleClass('sidebar-opposite-visible');

			// If visible
			if ($('body').hasClass('sidebar-opposite-visible')) {

				// Hide secondary
				$('body').addClass('sidebar-secondary-hidden');

			}
			else {

				// Show secondary
				$('body').removeClass('sidebar-secondary-hidden');
			}
		});

		// Hide all sidebars if opposite sidebar is shown
		$(document).on('click', '.sidebar-opposite-hide', function (e) {
			e.preventDefault();

			// Toggle sidebars visibility
			$('body').toggleClass('sidebar-all-hidden');

			// If hidden
			if ($('body').hasClass('sidebar-all-hidden')) {

				// Show opposite
				$('body').addClass('sidebar-opposite-visible');

				// Hide children lists
				$('.navigation-main').children('li').children('ul').css('display', '');
			}
			else {

				// Hide opposite
				$('body').removeClass('sidebar-opposite-visible');
			}
		});

		// Keep the width of the main sidebar if opposite sidebar is visible
		$(document).on('click', '.sidebar-opposite-fix', function (e) {
			e.preventDefault();

			// Toggle opposite sidebar visibility
			$('body').toggleClass('sidebar-opposite-visible');
		});
	};

	var setupMobileSidebar = function () {
		appOpts.log('executing setupMobileSidebar()...');

		// -------------------------
		// Mobile sidebar controls
		// -------------------------

		// Toggle main sidebar
		$('.sidebar-mobile-main-toggle').on('click', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-mobile-main').removeClass('sidebar-mobile-secondary sidebar-mobile-opposite sidebar-mobile-detached');
		});

		// Toggle secondary sidebar
		$('.sidebar-mobile-secondary-toggle').on('click', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-mobile-secondary').removeClass('sidebar-mobile-main sidebar-mobile-opposite sidebar-mobile-detached');
		});


		// Toggle opposite sidebar
		$('.sidebar-mobile-opposite-toggle').on('click', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-mobile-opposite').removeClass('sidebar-mobile-main sidebar-mobile-secondary sidebar-mobile-detached');
		});


		// Toggle detached sidebar
		$('.sidebar-mobile-detached-toggle').on('click', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-mobile-detached').removeClass('sidebar-mobile-main sidebar-mobile-secondary sidebar-mobile-opposite');
		});

		// -------------------------
		// Mobile sidebar setup
		// -------------------------
		$(window).on('resize', function () {
			setTimeout(function () {
				calculateContainerHeight();

				if ($(window).width() <= 768) {

					// Add mini sidebar indicator
					$('body').addClass('sidebar-xs-indicator');

					// Place right sidebar before content
					$('.sidebar-opposite').insertBefore('.content-wrapper');

					// Place detached sidebar before content
					$('.sidebar-detached').insertBefore('.content-wrapper');
				}
				else {

					// Remove mini sidebar indicator
					$('body').removeClass('sidebar-xs-indicator');

					// Revert back right sidebar
					$('.sidebar-opposite').insertAfter('.content-wrapper');

					// Remove all mobile sidebar classes
					$('body').removeClass('sidebar-mobile-main sidebar-mobile-secondary sidebar-mobile-detached sidebar-mobile-opposite');

					// Revert left detached position
					if ($('body').hasClass('has-detached-left')) {
						$('.sidebar-detached').insertBefore('.container-detached');
					}

						// Revert right detached position
					else if ($('body').hasClass('has-detached-right')) {
						$('.sidebar-detached').insertAfter('.container-detached');
					}
				}
			}, 100);
		}).resize();
	};

	var setupSidebarControls = function () {
		appOpts.log('executing setupSidebarControls()...');

		// -------------------------
		// Mini sidebar
		// -------------------------

		// Toggle mini sidebar
		$('.sidebar-main-toggle').on('click', function (e) {
			e.preventDefault();

			// Toggle min sidebar class
			$('body').toggleClass('sidebar-xs');
		});

		// -------------------------
		// Sidebar controls
		// -------------------------

		// Disable click in disabled navigation items
		$(document).on('click', '.navigation .disabled a', function (e) {
			e.preventDefault();
		});

		// Adjust page height on sidebar control button click
		$(document).on('click', '.sidebar-control', function (e) {
			calculateContainerHeight();
		});

		// Hide main sidebar in Dual Sidebar
		$(document).on('click', '.sidebar-main-hide', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-main-hidden');
		});

		// Toggle second sidebar in Dual Sidebar
		$(document).on('click', '.sidebar-secondary-hide', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-secondary-hidden');
		});

		// Hide detached sidebar
		$(document).on('click', '.sidebar-detached-hide', function (e) {
			e.preventDefault();
			$('body').toggleClass('sidebar-detached-hidden');
		});

		// Hide all sidebars
		$(document).on('click', '.sidebar-all-hide', function (e) {
			e.preventDefault();

			$('body').toggleClass('sidebar-all-hidden');
		});
	};

	var init = function (appOptions) {
		/// <summary>Initialize components.</summary>
		/// <param name="appOptions">Application option object.</param>

		// set the application options member...
		appOpts = appOptions;
		appOpts.log('setting up navigation bars...');

		// ----------------------------------------
		// Add code below to initialize component.
		// ----------------------------------------

		setupMainNavigation();
		calculateContainerHeight();
		setupNavbar();
		setupSidebar();
		setupSidebarCategories();
		setupOppositeSidebar();
		setupMobileSidebar();
		setupSidebarControls();
	};

	return {
		init : init
	};

}();