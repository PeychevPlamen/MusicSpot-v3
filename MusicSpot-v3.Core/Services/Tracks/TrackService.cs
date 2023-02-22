using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Core.Models.Tracks;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot_v3.Core.Services.Tracks
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext _context;

        public TrackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AllTracksViewModel> AllTracks(int albumId)
        {
            var tracks = await _context.Tracks.Where(a => a.AlbumId == albumId).ToListAsync();

            var result = new AllTracksViewModel
            {
                Tracks = tracks.OrderBy(a => a.Id).ToList(),
                AlbumId= albumId
            };

            return result;
        }

        public async Task<CreateTrackFormModel> CreateTrack(CreateTrackFormModel track)
        {
            var newTrack = new Track
            {
                Name = track.Name,
                Duration = track.Duration,
                AlbumId = track.AlbumId
            };

            await _context.Tracks.AddAsync(newTrack);
            await _context.SaveChangesAsync();

            return track;
        }
    }
}
