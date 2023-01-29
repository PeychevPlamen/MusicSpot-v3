using System;
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

        //// GET: Artists/Create
        //public IActionResult Create()
        //{
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: Artists/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Genre,Description,IsPublic,UserId")] Artist artist)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(artist);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artist.UserId);
        //    return View(artist);
        //}

        //// GET: Artists/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Artists == null)
        //    {
        //        return NotFound();
        //    }

        //    var artist = await _context.Artists.FindAsync(id);
        //    if (artist == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artist.UserId);
        //    return View(artist);
        //}

        //// POST: Artists/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Description,IsPublic,UserId")] Artist artist)
        //{
        //    if (id != artist.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(artist);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ArtistExists(artist.Id))
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
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artist.UserId);
        //    return View(artist);
        //}

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

        //private bool ArtistExists(int id)
        //{
        //    return _service.Artists.Any(e => e.Id == id);
        //}
    }
}
