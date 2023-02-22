using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Core.Models.Albums
{
    public class CreateAlbumFormModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(AlbumMaxNameLength)]
        public string? Title { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string? TitleImage { get; set; }

        [Range(MinYearValue, MaxYearValue)]
        public int Year { get; set; }

        [Required]
        public string? Format { get; set; }

        [Required]
        [Display(Name = "Media Condition")]
        public string? MediaCondition { get; set; }

        [Required]
        [Display(Name = "Sleeve Condition")]
        public string? SleeveCondition { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        public string? UserId { get; set; } // new
    }
}
