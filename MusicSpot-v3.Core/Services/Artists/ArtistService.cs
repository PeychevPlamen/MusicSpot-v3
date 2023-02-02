using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Core.Models.Artists;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MusicSpot_v3.Core.Services.Artists
{
    public class ArtistService : Controller, IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AllArtistsViewModel> AllArtists(string userId, string searchTerm, int p, int s)
        {
            var currArtist = await _context.Artists.Where(a => a.UserId == userId).ToListAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                currArtist = currArtist.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            var result = new AllArtistsViewModel()
            {
                Artists = currArtist
                .OrderBy(a => a.Name)
                .Skip(p * s - s)
                .Take(s)
                .ToList(),
                SearchTerm = searchTerm,
                PageNum = p,
                PageSize = s,
                TotalRec = currArtist.Count(),
                UserId = userId
            };

            return result;
        }

        public async Task<DetailsArtistFormModel> ArtistDetails(int? artistId)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == artistId);

            var result = new DetailsArtistFormModel()
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
                Description = artist.Description,
                IsPublic = artist.IsPublic
            };

            return result;
        }

        public bool ArtistExist(int artistId)
        {
            var artist = _context.Artists.Find(artistId);

            if (artist == null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Artist>> ArtistsList(string userId)
        {
            var artists = await _context.Artists.Where(x => x.UserId == userId).ToListAsync();

            return artists;
        }

        public async Task<CreateArtistFormModel> CreateArtist(CreateArtistFormModel model)
        {
            var newArtist = new Artist
            {
                Name = model.Name,
                Genre = model.Genre,
                Description = model.Description,
                IsPublic = model.IsPublic,
                UserId = model.UserId,
            };

            await _context.Artists.AddAsync(newArtist);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<DetailsArtistFormModel> DeleteArtist(int? artistId, DetailsArtistFormModel artist)
        {
            var artistToDelete = await _context.Artists.FindAsync(artistId);

            //if (artistToDelete == null)
            //{
            //    return ;
            //}

            _context.Artists.Remove(artistToDelete);
            await _context.SaveChangesAsync();

            return artist;
        }

        public async Task<EditArtistFormModel> EditArtist(int? artistId, EditArtistFormModel artist)
        {
            var artistData = _context.Artists.Find(artistId);

            //var result = new Artist
            //{
            //    Name = artist.Name,
            //    Genre = artist.Genre,
            //    Description = artist.Description,
            //    IsPublic = artist.IsPublic
            //};

            artistData.Name = artist.Name;
            artistData.Genre = artist.Genre;
            artistData.Description = artist.Description;
            artistData.IsPublic = artist.IsPublic;

            await _context.SaveChangesAsync();

            return artist;
        }

        //public async Task<EditArtistFormModel> EditArtist(EditArtistFormModel artist)
        //{
        //    var artistData = _context.Artists.Find(artist.Id);

        //    //if (artistData == null)
        //    //{
        //    //    return;
        //    //}


        //    artistData.Name = artist.Name;
        //    artistData.Genre = artist.Genre;
        //    artistData.Description = artist.Description;
        //    artistData.IsPublic = artist.IsPublic;

        //    await _context.SaveChangesAsync();

        //    return artist;
        //}

        //public async Task<EditArtistFormModel> EditArtist(int id, string? name, string? genre, string? description)
        //{
        //    var artistData = _context.Artists.Find(id);

        //    //if (artistData == null)
        //    //{
        //    //    return;
        //    //}


        //    artistData.Name = name;
        //    artistData.Genre = genre;
        //    artistData.Description = description;
        //    //artistData.IsPublic = artist.IsPublic;

        //    await _context.SaveChangesAsync();

        //    return artistData;
        //}
    }
}
