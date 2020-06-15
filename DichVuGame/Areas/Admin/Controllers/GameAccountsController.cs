using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DichVuGame.Data;
using DichVuGame.Models;

namespace DichVuGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GameAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GameAccounts.Include(g => g.Game);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/GameAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAccount = await _context.GameAccounts
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameAccount == null)
            {
                return NotFound();
            }

            return View(gameAccount);
        }

        // GET: Admin/GameAccounts/Create
        public IActionResult Create()
        {
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "Gamename");
            return View();
        }

        // POST: Admin/GameAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameID,Username,Password,Available")] GameAccount gameAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "Gamename", gameAccount.GameID);
            return View(gameAccount);
        }

        // GET: Admin/GameAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAccount = await _context.GameAccounts.FindAsync(id);
            if (gameAccount == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "Gamename", gameAccount.GameID);
            return View(gameAccount);
        }

        // POST: Admin/GameAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameID,Username,Password,Available")] GameAccount gameAccount)
        {
            if (id != gameAccount.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameAccountExists(gameAccount.ID))
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
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "Gamename", gameAccount.GameID);
            return View(gameAccount);
        }

        // GET: Admin/GameAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAccount = await _context.GameAccounts
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameAccount == null)
            {
                return NotFound();
            }

            return View(gameAccount);
        }

        // POST: Admin/GameAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameAccount = await _context.GameAccounts.FindAsync(id);
            _context.GameAccounts.Remove(gameAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameAccountExists(int id)
        {
            return _context.GameAccounts.Any(e => e.ID == id);
        }
    }
}
