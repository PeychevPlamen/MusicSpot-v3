using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Core.Models.Tracks;
using MusicSpot_v3.Core.Services.Albums;
using MusicSpot_v3.Core.Services.Tracks;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using MusicSpot_v3.Infrastructure.Extensions;

namespace MusicSpot_v3.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IAlbumService _albumService;

        public TracksController(ITrackService trackService, IAlbumService albumService)
        {
            _trackService = trackService;
            _albumService = albumService;
        }


        // GET: Tracks
        [Authorize]
        public async Task<IActionResult> Index(string searchTerm, int p = 1, int s = 5)
        {
            var userId = User.Id();

            var currTracks = await _trackService.Index(userId, searchTerm, p, s);

            return View(currTracks);
        }

        // GET: Tracks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _trackService.Details(id);

            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var album = await _albumService.Albums(id);

            ViewData["AlbumId"] = new SelectList(album, "Id", "Title");

            return View();
        }

        // POST: Tracks/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTrackFormModel track)
        {
            var album = await _albumService.Albums(track.AlbumId);

            if (ModelState.IsValid)
            {
                await _trackService.CreateTrack(track);

                return RedirectToAction(nameof(Create));
            }

            ViewData["AlbumId"] = new SelectList(album, "Id", "Title", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var track = await _trackService.Details(id);
            var albumList = await _albumService.Albums(id);

            if (_trackService.EditTrack == null)
            {
                return NotFound();
            }

            if (track == null)
            {
                return NotFound();
            }

            //ViewData["AlbumId"] = new SelectList(albumList, "Id", "Name", track.AlbumId);

            return View(track);
        }

        // POST: Tracks/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTrackFormModel track)
        {
            var albumList = await _albumService.Albums(track.AlbumId);

            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _trackService.EditTrack(id, track);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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

            ViewData["AlbumId"] = new SelectList(albumList, "Id", "Name", track.AlbumId);

            return View(track);
        }

        // GET: Tracks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _trackService.Details(id);

            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DetailsTrackFormModel track)
        {
            if (_trackService.Details(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tracks'  is null.");
            }

            var trackExist = _trackService.TrackExist(id);

            if (trackExist)
            {
                await _trackService.DeleteTrack(id, track);
            }
            else
            {
                return NotFound(nameof(track));
            }
                       
            return RedirectToAction(nameof(AllTracks));
        }

        private bool TrackExists(int id)
        {
            return _trackService.TrackExist(id);
        }

        [Authorize]
        public async Task<IActionResult> AllTracks(int id)
        {
            var tracks = await _trackService.AllTracks(id);

            return View(tracks);
        }
    }
}
