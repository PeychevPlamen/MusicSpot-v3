using System.ComponentModel.DataAnnotations;

namespace MusicSpot_v3.Core.Models.Artists
{
    public class AllArtistsViewModel
    {
        public IEnumerable<string>? ArtistsName { get; set; }

        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }

        public IEnumerable<Infrastructure.Data.Models.Artist>? Artists { get; set; }

        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public int TotalRec { get; set; }

        public string? UserId { get; set; }
    }
}
