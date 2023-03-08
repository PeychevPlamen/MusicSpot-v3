using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicSpot_v3.Core.Models.Tracks
{
    public class DetailsTrackFormModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Duration { get; set; }

        public int? AlbumId { get; set; }

        public Album? Album { get; set; }

        public string? AlbumTitle { get; set; }
    }
}
