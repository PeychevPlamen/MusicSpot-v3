using Microsoft.AspNetCore.Identity;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicSpot_v3.Infrastructure.Data.DataConstants;

namespace MusicSpot_v3.Infrastructure.Data.Identity
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UsernameMaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string? LastName { get; set; }

        public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    }
}
