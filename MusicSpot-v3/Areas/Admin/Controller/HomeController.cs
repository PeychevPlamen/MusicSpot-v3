using Microsoft.AspNetCore.Mvc;
using MusicSpot_v3.Core.Models.Artists;
using MusicSpot_v3.Infrastructure.Data;
using MusicSpot_v3.Infrastructure.Extensions;
using System.Data.Entity;



namespace MusicSpot_v3.Areas.Admin.Controller
{
    public class HomeController : AdminController
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            if (User.IsAdmin())
            {
                var currArtist = await _context.Artists.ToListAsync();

                return View(new AllArtistsViewModel
                {
                    Artists = currArtist,
                });
            }

            return View(_context.Artists.AsQueryable());
        }

    }
}
