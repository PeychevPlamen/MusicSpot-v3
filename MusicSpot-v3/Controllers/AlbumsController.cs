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
        public async Task<IActionResult> Index(int id, string searchTerm, int p = 1, int s = 5)
        {
            var userId = User.Id();

            var currAlbums = await _albumService.TotalUserAlbums(userId, searchTerm, p, s);

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
                var result = await _albumService.CreateAlbum(userId, album);

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

            ViewData["ArtistId"] = new SelectList(artistsList, "Id", "Name", album.ArtistId);

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

        // GET: Albums/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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

        // POST: Albums/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DetailsAlbumFormModel album)
        {
            if (_albumService.DetailsAlbum(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Albums'  is null.");
            }

            var albumToDelete = _albumService.AlbumExists(id);

            if (albumToDelete)
            {
                await _albumService.DeleteAlbum(id, album);
            }
            else
            {
                return NotFound(nameof(album));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _albumService.AlbumExists(id);
        }

        public async Task<IActionResult> AllAlbums(int id, string searchTerm, int p = 1, int s = 5)
        {
            var userId = User.Id();
            var albums = await _albumService.AllAlbums(userId, id, searchTerm, p, s);

            return View(albums);
        }
    }
}
