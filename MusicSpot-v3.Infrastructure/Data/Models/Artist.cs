using MusicSpot_v3.Infrastructure.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Infrastructure.Data.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ArtistMaxNameLength)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(GenreMaxLength)]
        public string? Genre { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public ICollection<Album> Albums { get; set; } = new List<Album>();

        [Required]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public User? User { get; set; }
    }
}
