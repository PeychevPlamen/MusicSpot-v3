using System.ComponentModel.DataAnnotations;

using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Core.Models.Artists
{
    public class EditArtistFormModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(ArtistMaxNameLength)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(GenreMaxLength)]
        public string? Genre { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        //public string? UserId { get; set; }
    }
}
