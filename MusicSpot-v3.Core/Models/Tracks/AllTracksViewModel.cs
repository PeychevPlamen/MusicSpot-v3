using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicSpot_v3.Infrastructure.Data.Models;

namespace MusicSpot_v3.Core.Models.Tracks
{
    public class AllTracksViewModel
    {
        //public IEnumerable<string> TrackName { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public int AlbumId { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public int TotalRec { get; set; }

        //public string? UserId { get; set; }
    }
}
