using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DichVuGame.Data;
using DichVuGame.Models;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using DichVuGame.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DichVuGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudiosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IWebHostEnvironment _hostEnvironment;
        public StudiosController(ApplicationDbContext context,IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostEnvironment = hostingEnvironment;
        }

        // GET: Admin/Studios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Studios.Include(s => s.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Studios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // GET: Admin/Studios/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Countryname");
            return View();
        }

        // POST: Admin/Studios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Studioname,StudioLogo,Describe,CountryID")] Studio studio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studio);
                await _context.SaveChangesAsync();
                var webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    var path = Path.Combine(webRootPath, SD.StudioImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(path, studio.ID + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    studio.StudioLogo = @"\" + SD.StudioImageFolder + @"\" + studio.ID + extension;
                }
                else
                {
                    var path = Path.Combine(webRootPath, SD.DefaultStudioImage);
                    System.IO.File.Copy(path,webRootPath + @"\" + SD.StudioImageFolder + @"\" + studio.ID + ".jpeg");
                    studio.StudioLogo = @"\" + SD.StudioImageFolder + @"\" + studio.ID + ".jpeg";
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "ID", studio.CountryID);
            return View(studio);
        }

        // GET: Admin/Studios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios.FindAsync(id);
            if (studio == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "ID", studio.CountryID);
            return View(studio);
        }

        // POST: Admin/Studios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Studioname,StudioLogo,Describe,CountryID")] Studio studio)
        {
            if (id != studio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studio);
                    await _context.SaveChangesAsync();
                    var webRootPath = _hostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    if(files.Count > 0)
                    {
                        if(studio.StudioLogo != null)
                        {
                            var old = webRootPath + @"" + studio.StudioLogo;
                            System.IO.File.Delete(old);
                        }
                        var path = Path.Combine(webRootPath, SD.StudioImageFolder);
                        var extension = Path.GetExtension(files[0].FileName);
                        using(var fileStream = new FileStream(Path.Combine(path,studio.ID + extension),FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        studio.StudioLogo = @"\" + SD.StudioImageFolder + @"\" + studio.ID + extension;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioExists(studio.ID))
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
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "ID", studio.CountryID);
            return View(studio);
        }

        // GET: Admin/Studios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // POST: Admin/Studios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studio = await _context.Studios.FindAsync(id);
            System.IO.File.Delete(_hostEnvironment.WebRootPath + @"\" + studio.StudioLogo);
            _context.Studios.Remove(studio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudioExists(int id)
        {
            return _context.Studios.Any(e => e.ID == id);
        }
    }
}
