using CommonLib.Configs;
using StatTrack.BLL.DataManagers.Settings;

namespace StatTrack.WEB.Plumbing.Config
{

    public class AppSettings : IAppSettings
    {

        #region Ctor

        /// <summary>
        /// Build the application settings object.
        /// </summary>
        public AppSettings(string basePath, string baseUrl)
        {
            BasePath = basePath;
            BaseUrl = baseUrl;
        }

        #endregion

        #region Application settings

        private string _contentFolder;
        private string _appName;

        /// <summary>
        /// Base application URL that is used to build URLs from the service layer.
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Application base path.
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Content folder name.
        /// </summary>
        public string ContentFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_contentFolder))
                {
                    _contentFolder = ConfigHelper.Get<string>("ContentFolder");
                }
                return _contentFolder;
            }
        }

        /// <summary>
        /// Application name.
        /// </summary>
        public string AppName
        {
            get
            {
                if (string.IsNullOrEmpty(_appName))
                {
                    _appName = ConfigHelper.Get<string>("ApplicationName");
                }
                return _appName;
            }
        }

        #endregion

        #region Account email settings

        private string _emailConfirmTokenUrl;
        private string _resetPasswordTokenUrl;

        /// <summary>
        /// Email confirmation url.
        /// </summary>
        public string EmailConfirmTokenUrlTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_emailConfirmTokenUrl))
                {
                    _emailConfirmTokenUrl = BaseUrl + ConfigHelper.Get<string>("EmailConfirmTokenUrlTemplate");
                }
                return _emailConfirmTokenUrl;
            }
        }

        /// <summary>
        /// Reset password token url.
        /// </summary>
        public string ResetPasswordTokenUrlTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_resetPasswordTokenUrl))
                {
                    _resetPasswordTokenUrl = BaseUrl + ConfigHelper.Get<string>("ResetPasswordTokenUrlTemplate");
                }
                return _resetPasswordTokenUrl;
            }
        }

        #endregion

        #region Security settings

        private short? _maxFailedAccessAttemptsBeforeLockout;
        private short? _accountLockoutTimeSpanMinutes;
        private bool? _userLockoutEnabledByDefault;

        /// <summary>
        /// Maximum failed attempts before we lock out an account. Default value is 5.
        /// </summary>
        public short MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                if (_maxFailedAccessAttemptsBeforeLockout == null)
                {
                    _maxFailedAccessAttemptsBeforeLockout = ConfigHelper.Get<short>("MaxFailedAccessAttemptsBeforeLockout");
                }

                return _maxFailedAccessAttemptsBeforeLockout.Value;
            }
        }

        /// <summary>
        /// Maximum lockout time if the users tried and reached the maximum failed attempts to login. Default is 15.
        /// </summary>
        public short AccountLockoutTimeSpanMinutes
        {
            get
            {
                if (_accountLockoutTimeSpanMinutes == null)
                {
                    _accountLockoutTimeSpanMinutes = ConfigHelper.Get<short>("AccountLockoutTimeSpanMinutes");
                }

                return _accountLockoutTimeSpanMinutes.Value;
            }
        }

        /// <summary>
        /// True to enable lockout for users.
        /// </summary>
        public bool UserLockoutEnabledByDefault
        {
            get
            {
                if (_userLockoutEnabledByDefault == null)
                {
                    _userLockoutEnabledByDefault = ConfigHelper.Get<bool>("UserLockoutEnabledByDefault");
                }

                return _userLockoutEnabledByDefault.Value;
            }
        }

        #endregion

        #region User Profile

        private string _defaultUserAvatar;

        public string DefaultUserAvatar
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultUserAvatar))
                {
                    _defaultUserAvatar = BaseUrl + ConfigHelper.Get<string>("DefaultUserAvatar");
                }

                return _defaultUserAvatar;
            }
        }

        #endregion

    }
}
