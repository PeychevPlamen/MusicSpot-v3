using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Core.Models.Tracks
{
    public class EditTrackFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TrackMaxNameLength)]
        public string? Name { get; set; }

        [Required]
        [RegularExpression(TrackDurationRegEx, ErrorMessage = "Duration should be in format mm:ss")]
        public string? Duration { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public int AlbumId { get; set; }
    }
}
