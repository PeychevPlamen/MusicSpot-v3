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
        public async Task<IActionResult> Index(int albumId)
        {
            //var currAlbum = await _trackService.AllTracks(albumId);

            return View();
        }

        // GET: Tracks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _trackService.Details(id) == null)
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

        //// GET: Tracks/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Tracks == null)
        //    {
        //        return NotFound();
        //    }

        //    var track = await _context.Tracks.FindAsync(id);
        //    if (track == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Format", track.AlbumId);
        //    return View(track);
        //}

        //// POST: Tracks/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Duration,AlbumId")] Track track)
        //{
        //    if (id != track.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(track);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TrackExists(track.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Format", track.AlbumId);
        //    return View(track);
        //}

        //// GET: Tracks/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Tracks == null)
        //    {
        //        return NotFound();
        //    }

        //    var track = await _context.Tracks
        //        .Include(t => t.Album)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (track == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(track);
        //}

        //// POST: Tracks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Tracks == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Tracks'  is null.");
        //    }
        //    var track = await _context.Tracks.FindAsync(id);
        //    if (track != null)
        //    {
        //        _context.Tracks.Remove(track);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TrackExists(int id)
        //{
        //  return _context.Tracks.Any(e => e.Id == id);
        //}

        [Authorize]
        public async Task<IActionResult> AllTracks(int id)
        {
            var tracks = await _trackService.AllTracks(id);

            return View(tracks);
        }
    }
}
