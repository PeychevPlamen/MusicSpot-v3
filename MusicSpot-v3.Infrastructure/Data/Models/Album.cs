using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Infrastructure.Data.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AlbumMaxNameLength)]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string? TitleImage { get; set; }

        public int Year { get; set; }

        [Required]
        public string? Format { get; set; }

        [Display(Name = "Media Condition")]
        public string? MediaCondition { get; set; }

        [Display(Name = "Sleeve Condition")]
        public string? SleeveCondition { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        [Required]
        [ForeignKey(nameof(Artist))]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        public Artist? Artist { get; set; }

        public string? UserId { get; set; }

    }
}
