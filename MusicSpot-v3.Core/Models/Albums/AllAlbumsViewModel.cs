using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicSpot_v3.Infrastructure.Data.Models;

namespace MusicSpot_v3.Core.Models.Albums
{
    public class AllAlbumsViewModel
    {
        public IEnumerable<string>? AlbumName { get; set; }

        public IEnumerable<Album>? Albums { get; set; }

        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }

        public int PageNum { get; set; }

        public int TotalRec { get; set; }

        public int PageSize { get; set; }

        public int ArtistId { get; set; }

        public string? UserId { get; set; }
    }
}
