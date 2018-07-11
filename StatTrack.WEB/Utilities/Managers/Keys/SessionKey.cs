namespace StatTrack.WEB
{
	public class SessionKey
	{
		public static readonly SessionKey CurrentUser = new SessionKey("current_user");

		private SessionKey(string value)
		{
			Name = value;
		}

		public string Name { get; private set; }
	}
}