using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicSpot_v3.Core.Models.Albums
{
    public class DetailsAlbumFormModel
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        [Display(Name = "Image")]
        public string? TitleImage { get; set; }

        public int Year { get; set; }

        public string? Format { get; set; }

        public string? MediaCondition { get; set; }

        public string? SleeveCondition { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public Artist? Artist { get; set; }

        public int ArtistId { get; set; }
    }
}
