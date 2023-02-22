using MusicSpot_v3.Core.Models.Albums;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
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

        public bool AlbumExists(int albumId)
        {
            var album = _context.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                return false;
            }

            return true;
        }

        public async Task<AllAlbumsViewModel> AllAlbums(string userId, int artistId, string searchTerm, int p, int s)
        {
            var currAlbums = _context.Albums.Where(a => a.ArtistId == artistId).Include(a => a.Artist).AsQueryable();

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

        public async Task<CreateAlbumFormModel> CreateAlbum(CreateAlbumFormModel album)
        {
            var currAlbum = new Album
            {
                Id = album.Id,
                Title = album.Title,
                TitleImage= album.TitleImage,
                Year = album.Year,
                Format = album.Format,
                MediaCondition = album.MediaCondition,
                SleeveCondition = album.SleeveCondition,
                Description = album.Description,
                IsPublic = album.IsPublic,
                Tracks = album.Tracks,
                ArtistId = album.ArtistId,
            };

            await _context.Albums.AddAsync(currAlbum);
            await _context.SaveChangesAsync();

            return album;
        }

        public async Task<DetailsAlbumFormModel> DeleteAlbum(int? albumId, DetailsAlbumFormModel album)
        {
            var albumToDelete = await _context.Albums.FindAsync(albumId);

            //if (albumToDelete == null)
            //{
            //    return:
            //}

            _context.Albums.Remove(albumToDelete);
            await _context.SaveChangesAsync();

            return album;
        }

        public async Task<DetailsAlbumFormModel> DetailsAlbum(int? albumId)
        {
            var currAlbum = await _context.Albums.FindAsync(albumId);

            var result = new DetailsAlbumFormModel
            {
                Id = albumId,
                Title = currAlbum.Title,
                TitleImage = currAlbum.TitleImage,
                Year = currAlbum.Year,
                Format = currAlbum.Format,
                MediaCondition = currAlbum.MediaCondition,
                SleeveCondition= currAlbum.SleeveCondition,
                Description = currAlbum.Description,
                IsPublic = currAlbum.IsPublic,
                Tracks = currAlbum.Tracks,
                ArtistId = currAlbum.ArtistId
            };

            return result;
        }

        public async Task<EditAlbumFormModel> EditAlbum(int? albumId, EditAlbumFormModel album)
        {
            var currAlbum = await _context.Albums.FindAsync(albumId);

            currAlbum.Title = album.Title;
            currAlbum.TitleImage = album.TitleImage;
            currAlbum.Year = album.Year;
            currAlbum.Format = album.Format;
            currAlbum.MediaCondition = album.MediaCondition;
            currAlbum.SleeveCondition = album.SleeveCondition;
            currAlbum.Description = album.Description;
            currAlbum.IsPublic = album.IsPublic;
            currAlbum.Tracks = album.Tracks;
            currAlbum.ArtistId = album.ArtistId;

            await _context.SaveChangesAsync();

            return album;
        }

        public async Task<List<Album>> Albums(int id)
        {
            var album = await _context.Albums.Where(x => x.Id == id).ToListAsync();

            return album;
        }
    }
}
