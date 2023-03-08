using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Core.Models.Tracks;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                AlbumId = albumId
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

        public async Task<DetailsTrackFormModel> DeleteTrack(int? trackId, DetailsTrackFormModel trackToDelete)
        {
            var track = await _context.Tracks.FindAsync(trackId);

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();

            return trackToDelete;
        }

        public async Task<DetailsTrackFormModel> Details(int? id)
        {
            var track = await _context.Tracks.FindAsync(id);

            var albumTitle = await _context.Tracks.Where(t => t.AlbumId == track.AlbumId).Select(t => t.Album.Title).FirstOrDefaultAsync();

            var result = new DetailsTrackFormModel
            {
                Id = track.Id,
                Name = track?.Name,
                Duration = track?.Duration,
                AlbumId = track?.AlbumId,
                AlbumTitle = albumTitle,
            };

            return result;
        }

        public async Task<EditTrackFormModel> EditTrack(int? id, EditTrackFormModel track)
        {
            var currTrack = await _context.Tracks.FindAsync(id);

            currTrack.Name = track.Name;
            currTrack.Duration = track.Duration;
            //currTrack.AlbumId = track.AlbumId;

            await _context.SaveChangesAsync();

            return track;
        }

        public async Task<AllTracksViewModel> Index(string userId, string searchTerm, int p, int s)
        {
            var allTracks = _context.Albums.Where(x => x.Artist.UserId == userId).SelectMany(x => x.Tracks);
            var albumId = await _context.Albums.Select(a => a.Id).FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                allTracks = allTracks.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var result = new AllTracksViewModel
            {
                Tracks = allTracks
                              .OrderBy(x => x.Name)
                              .Skip(p * s - s)
                              .Take(s)
                              .ToList(),
                SearchTerm = searchTerm,
                PageNum = p,
                PageSize = s,
                TotalRec = allTracks.ToList().Count(),
                UserId = userId,
                AlbumId = albumId
            };

            return result;
        }

        public bool TrackExist(int trackId)
        {
            var track = _context.Tracks.FirstOrDefault(t => t.Id == trackId);

            if (track == null)
            {
                return false;
            }

            return true;
        }
    }
}
