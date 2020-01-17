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
    public class GatuneksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GatuneksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gatuneks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gatunek.ToListAsync());
        }



        // GET: Gatuneks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatunek = await _context.Gatunek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gatunek == null)
            {
                return NotFound();
            }
            var slabo = await _context.Film.ToListAsync();

            var result = from a in slabo where a.Id.Equals(id) select a;
            bool isEmpty = !result.Any();

            if (isEmpty)
            {
                return View(gatunek);

            }
            return View("DetailsList",slabo);


        }
        [Authorize(Roles = "Administrator, Moderator")]

        // GET: Gatuneks/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Gatuneks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Gatunek gatunek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gatunek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gatunek);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // GET: Gatuneks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatunek = await _context.Gatunek.FindAsync(id);
            if (gatunek == null)
            {
                return NotFound();
            }
            return View(gatunek);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Gatuneks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Gatunek gatunek)
        {
            if (id != gatunek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gatunek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatunekExists(gatunek.Id))
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
            return View(gatunek);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // GET: Gatuneks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatunek = await _context.Gatunek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gatunek == null)
            {
                return NotFound();
            }

            return View(gatunek);
        }
        [Authorize(Roles = "Administrator, Moderator")]

        // POST: Gatuneks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gatunek = await _context.Gatunek.FindAsync(id);
            _context.Gatunek.Remove(gatunek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GatunekExists(int id)
        {
            return _context.Gatunek.Any(e => e.Id == id);
        }
    }
}
