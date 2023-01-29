using MusicSpot_v3.Infrastructure.Data.Models;

namespace MusicSpot_v3.Core.Models.Artists
{
    public class ArtistsListFormModel
    {
        public IEnumerable<Artist>? Artists { get; set; }

        public string UserId { get; set; }
    }
}
