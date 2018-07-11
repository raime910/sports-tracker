namespace StatTrack.BLL.DataFactory
{
	internal class Factories
	{
		private SportTypeFactory _sportTypeFactory;
		private SportFactory _sportFactory;

		internal SportTypeFactory SportTypeFactory => _sportTypeFactory ?? (_sportTypeFactory = new SportTypeFactory());

		internal SportFactory SportFactory => _sportFactory ?? (_sportFactory = new SportFactory());
	}
}
