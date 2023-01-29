using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot_v3.Core.Models.Artists
{
    public class CreateArtistFormModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Genre { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public string? UserId { get; set; }
    }
}
