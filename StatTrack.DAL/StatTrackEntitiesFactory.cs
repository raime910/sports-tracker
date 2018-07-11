using System.Data.Entity.Infrastructure;

namespace StatTrack.DAL
{
	public class StatTrackEntitiesFactory : IDbContextFactory<StatTrackEntities>
	{
		public StatTrackEntities Create()
		{
			return new StatTrackEntities();
		}
	}
}
