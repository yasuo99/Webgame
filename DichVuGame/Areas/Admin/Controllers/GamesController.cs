using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DichVuGame.Data;
using DichVuGame.Models;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using DichVuGame.Utility;

namespace DichVuGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        [BindProperty]
        public GamesViewModel GamesViewModel { get; set; }
        public GamesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
            GamesViewModel = new GamesViewModel()
            {
                Countries = _context.Countries.ToList(),
                GameTags = _context.GameTags.ToList(),
                Studios = _context.Studios.ToList(),
                Games = _context.Games.ToList(),
                Game = new Game(),
                Studio = new Studio(),
                SystemRequirement = new SystemRequirement()
            };
        }

        // GET: Admin/Games
        public ActionResult Index()
        {
            return View(GamesViewModel);
        }

        // GET: Admin/Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Studio)
                .Include(g => g.SystemRequirement)
                .FirstOrDefaultAsync(m => m.ID == id);
            var studio = await _context.Studios.Where(s => s.ID == game.StudioID).FirstOrDefaultAsync();
            var gameTag = await _context.GameTags.Where(g => g.GameID == game.ID).ToListAsync();
            if (game == null)
            {
                return NotFound();
            }
            GamesViewModel.Game = game;
            GamesViewModel.Studio = studio;
            GamesViewModel.GameTags = gameTag;
            return View(GamesViewModel);
        }

        // GET: Admin/Games/Create
        public IActionResult Create()
        {
            ViewData["StudioID"] = new SelectList(_context.Studios, "ID", "Studioname");
            ViewData["SystemRequirementID"] = new SelectList(_context.SystemRequirements, "ID", "ID");
            return View();
        }

        // POST: Admin/Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                _context.Games.Add(GamesViewModel.Game);
                await _context.SaveChangesAsync();

                var gameFromDb = _context.Games.Find(GamesViewModel.Game.ID);
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(GamesViewModel.Tags != null)
                {
                    List<string> tags = HandingBareTag(GamesViewModel.Tags);
                    foreach (var temptag in tags)
                    {
                        Tag tag = new Tag()
                        {
                            Tagname = temptag
                        };
                        _context.Tags.Add(tag);
                        await _context.SaveChangesAsync();
                        GameTag gameTag = new GameTag()
                        {
                            GameID = GamesViewModel.Game.ID,
                            TagID = tag.ID
                        };
                        _context.GameTags.Add(gameTag);
                        await _context.SaveChangesAsync();
                    }
                }
                if (files.Count != 0)
                {
                    var uploads = Path.Combine(webRootPath, SD.GameImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, GamesViewModel.Game.ID + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    gameFromDb.GamePoster = @"\" + SD.GameImageFolder + @"\" + GamesViewModel.Game.ID + extension;
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, SD.GameImageFolder + @"\" + SD.DefaultGameImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.GameImageFolder + @"\" + GamesViewModel.Game.ID + ".png");
                    gameFromDb.GamePoster = @"\" + SD.GameImageFolder + @"\" + GamesViewModel.Game.ID + ".png";
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudioID"] = new SelectList(_context.Studios, "ID", "Studioname", GamesViewModel.Game.StudioID);
            return View(GamesViewModel);
        }

        // GET: Admin/Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["StudioID"] = new SelectList(_context.Studios, "ID", "ID", game.StudioID);
            return View(game);
        }

        // POST: Admin/Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Gamename,Release,StudioID,Price,Available,SystemRequirementID")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    _context.Update(game);
                    if (files.Count > 0)
                    {
                        if (game.GamePoster != null)
                        {
                            var old = webRootPath + @"" + game.GamePoster;
                            System.IO.File.Delete(old);
                        }
                        var path = Path.Combine(webRootPath, SD.GameImageFolder);
                        var extension = Path.GetExtension(files[0].FileName);
                        using(var fileStream = new FileStream(Path.Combine(path,game.ID + extension),FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        game.GamePoster = @"\" + SD.GameImageFolder + @"\" + game.ID + extension;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
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
            ViewData["StudioID"] = new SelectList(_context.Studios, "ID", "ID", game.StudioID);
            return View(game);
        }

        // GET: Admin/Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Studio)
                .Include(g => g.SystemRequirement)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Admin/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            System.IO.File.Delete(_hostingEnvironment.WebRootPath + @"" + game.GamePoster);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
        public List<string> HandingBareTag(string bareTag)
        {
            List<string> tag = new List<string>();
            int start = 0;
            int end = 0;
            while (start < bareTag.Length)
            {
                string temp = "";
                for (int j = start; j < bareTag.Length; j++)
                {
                    if (bareTag[j] == ',')
                    {
                        end = j;
                        break;
                    }
                    if (j == bareTag.Length - 1)
                    {
                        end = j + 1;
                        break;
                    }
                }
                for (int k = start; k < end; k++)
                {
                    temp += bareTag[k];
                }
                start = end + 1;
                tag.Add(temp.Trim());
            }
            return tag;
        }
    }
}
