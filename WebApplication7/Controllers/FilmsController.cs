using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Authorize]
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Film.Include(f => f.Gatunek);
            return View(await applicationDbContext.ToListAsync());
        }




        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Gatunek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
        [Authorize(Roles ="Administrator, Moderator")]
        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["GatunekId"] = new SelectList(_context.Gatunek, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,ReleaseDate,GatunekId")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GatunekId"] = new SelectList(_context.Gatunek, "Id", "Name", film.GatunekId);
            return View(film);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["GatunekId"] = new SelectList(_context.Gatunek, "Id", "Name", film.GatunekId);
            return View(film);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,ReleaseDate,GatunekId")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            ViewData["GatunekId"] = new SelectList(_context.Gatunek, "Id", "Name", film.GatunekId);
            return View(film);
        }


        public async Task<IActionResult> Szukaj(string szukaj)
        {
            var movies = from m in _context.Film
                         select m;
            var applicationDbContext = _context.Film.Include(f => f.Gatunek);

            if (!String.IsNullOrEmpty(szukaj))
            {
                movies = movies.Where(s => s.Tytul.Contains(szukaj));
            }

            return View(await movies.ToListAsync());
        }
    

        [Authorize(Roles = "Administrator, Moderator")]

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Gatunek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
