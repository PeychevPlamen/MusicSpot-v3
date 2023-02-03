using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Core.Models.Albums;
using MusicSpot_v3.Core.Services.Albums;
using MusicSpot_v3.Core.Services.Artists;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using MusicSpot_v3.Infrastructure.Extensions;

namespace MusicSpot_v3.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;

        public AlbumsController(IArtistService artistService, IAlbumService albumService)
        {
            _artistService = artistService;
            _albumService = albumService;
        }


        // GET: Albums
        [Authorize]
        public async Task<IActionResult> Index(int artistId, string searchTerm, int p = 1, int s = 5)
        {
            var userId = User.Id();

            var currAlbums = await _albumService.AllAlbums(userId, artistId, searchTerm, p, s);

            return View(currAlbums);
        }

        // GET: Albums/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _albumService.DetailsAlbum == null)
            {
                return NotFound();
            }

            var album = await _albumService.DetailsAlbum(id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var userId = User.Id();

            var artistList = await _artistService.ArtistsList(userId);

            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");

            return View();
        }

        // POST: Albums/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAlbumFormModel album)
        {
            var userId = User.Id();
            var artistsList = await _artistService.ArtistsList(userId);

            if (ModelState.IsValid)
            {
                var result = await _albumService.CreateAlbum(album);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(artistsList, "Id", "Name", album.ArtistId);

            return View(album);
        }

        // GET: Albums/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.Id();
            var artistsList = await _artistService.ArtistsList(userId);

            if (id == null || _albumService.EditAlbum == null)
            {
                return NotFound();
            }

            var album = await _albumService.DetailsAlbum(id);

            if (album == null)
            {
                return NotFound();
            }

            ViewData["ArtistId"] = new SelectList(artistsList, "Id", "Genre", album.ArtistId);

            return View(album);
        }

        // POST: Albums/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EditAlbumFormModel album)
        {
            var userId = User.Id();
            var artistsList = await _artistService.ArtistsList(userId);

            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _albumService.EditAlbum(id, album);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(artistsList, "Id", "Genre", album.ArtistId);

            return View(album);
        }

        //// GET: Albums/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Albums == null)
        //    {
        //        return NotFound();
        //    }

        //    var album = await _context.Albums
        //        .Include(a => a.Artist)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(album);
        //}

        //// POST: Albums/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Albums == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Albums'  is null.");
        //    }
        //    var album = await _context.Albums.FindAsync(id);
        //    if (album != null)
        //    {
        //        _context.Albums.Remove(album);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AlbumExists(int id)
        {
            return _albumService.AlbumExists(id);
        }
    }
}
