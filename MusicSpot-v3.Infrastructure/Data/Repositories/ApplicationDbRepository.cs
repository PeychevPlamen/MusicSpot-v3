using MusicSpot_v3.Infrastructure.Data.Common;

namespace MusicSpot_v3.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(ApplicationDbContext context) 
        {
            this.Context = context;
        }
    }
}
