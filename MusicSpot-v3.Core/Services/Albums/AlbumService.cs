using MusicSpot_v3.Core.Models.Albums;
using MusicSpot_v3.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot_v3.Core.Services.Albums
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _context;

        public AlbumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AllAlbumsViewModel> AllAlbums(string userId, int artistId, string searchTerm, int p, int s)
        {
            var currAlbums = _context.Albums.Where(a => a.Artist.UserId == userId).Include(a => a.Artist).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                currAlbums = currAlbums.Where(a => a.Title.ToLower().Contains(searchTerm.ToLower()) ||
                a.Year.ToString().ToLower().Contains(searchTerm.ToLower()) ||
                a.Format.ToLower().Contains(searchTerm.ToLower()));
            }

            var result = new AllAlbumsViewModel
            {
                Albums = currAlbums
                            .OrderBy(x => x.Title)
                            .Skip(p * s - s)
                            .Take(s)
                            .ToList(),
                SearchTerm = searchTerm,
                PageNum = p,
                PageSize = s,
                TotalRec = currAlbums.Count(),
                ArtistId = artistId,
                UserId = userId
            };

            return result;
        }
    }
}
