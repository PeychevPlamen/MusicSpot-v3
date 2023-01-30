﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Data.Models;
using MusicSpot_v3.Infrastructure.Data.Identity;
using MusicSpot_v3.Core.Services.Artists;
using MusicSpot_v3.Core.Models.Artists;
using MusicSpot_v3.Areas.Identity;
using MusicSpot_v3.Infrastructure.Extensions;

namespace MusicSpot_v3.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService _service;

        public ArtistsController(IArtistService service)
        {
            _service = service;
        }


        // GET: Artists
        [Authorize]
        public async Task<IActionResult> Index(string userId, string searchTerm, int p = 1, int s = 5)
        {

            var userID = User.Id();

            var model = await _service.AllArtists(userID, searchTerm, p, s);

            return View(model);
        }

        // GET: Artists/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _service.ArtistDetails(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Artists/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Artists/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArtistFormModel artist)
        {
            var userId = User.Id();
            artist.UserId = userId;

            if (ModelState.IsValid)
            {
                var result = _service.CreateArtist(artist);

                return RedirectToAction(nameof(Index));
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _service.ArtistDetails == null)
            {
                return NotFound();
            }

            var artist = await _service.ArtistDetails(id);

            if (artist == null)
            {
                return NotFound();
            }

            // var currArtist = new Artist { Name= artist.Name, Genre = artist.Genre, Description = artist.Description, IsPublic = artist.IsPublic };

            return View(artist);
        }

        // POST: Artists/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EditArtistFormModel artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            var editedArtist = await _service.EditArtist(id, artist);

            if (editedArtist == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.EditArtist(id, artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
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

            return View(artist);
        }

        //// GET: Artists/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Artists == null)
        //    {
        //        return NotFound();
        //    }

        //    var artist = await _context.Artists
        //        .Include(a => a.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (artist == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(artist);
        //}

        //// POST: Artists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Artists == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Artists'  is null.");
        //    }
        //    var artist = await _context.Artists.FindAsync(id);
        //    if (artist != null)
        //    {
        //        _context.Artists.Remove(artist);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ArtistExists(int id)
        {
            return _service.ArtistExist(id);
        }
    }
}
