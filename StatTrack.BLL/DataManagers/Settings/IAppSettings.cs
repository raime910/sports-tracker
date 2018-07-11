namespace StatTrack.BLL.DataManagers.Settings
{
    public interface IAppSettings
    {
        short AccountLockoutTimeSpanMinutes { get; }
        string AppName { get; }
        string BasePath { get; }
        string BaseUrl { get; }
        string ContentFolder { get; }
        string DefaultUserAvatar { get; }
        string EmailConfirmTokenUrlTemplate { get; }
        short MaxFailedAccessAttemptsBeforeLockout { get; }
        string ResetPasswordTokenUrlTemplate { get; }
        bool UserLockoutEnabledByDefault { get; }
    }
}