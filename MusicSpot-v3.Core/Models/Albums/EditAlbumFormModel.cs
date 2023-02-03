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
    public class EditAlbumFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AlbumMaxNameLength)]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Image")]
        [Url]
        public string? TitleImage { get; set; }

        [Range(MinYearValue, MaxYearValue)]
        public int Year { get; set; }

        [Required]
        public string? Format { get; set; }

        [Display(Name = "Media Condition")]
        public string? MediaCondition { get; set; }

        [Display(Name = "Sleeve Condition")]
        public string? SleeveCondition { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public int ArtistId { get; set; }

    }
}
