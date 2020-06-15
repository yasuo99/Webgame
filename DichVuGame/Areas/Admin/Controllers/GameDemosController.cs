using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Models;
using DichVuGame.Models.ViewModels;
using DichVuGame.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DichVuGame.Areas.Admin.Controllers
{
    public class GameDemosController : Controller
    {
        private ApplicationDbContext _db;
        [BindProperty]
        public GamesViewModel GamesVM { get; set; }
        public IWebHostEnvironment _hostEnvironment;
        public GameDemosController(ApplicationDbContext db,IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            GamesVM = new GamesViewModel()
            {
                Game = new Game(),
                GameDemo = new GameDemo(),
                GameDemos = new List<GameDemo>()
            };
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int? id)
        {
            var game = await _db.Games.Where(u => u.ID == id).FirstOrDefaultAsync();
            GamesVM.Game = game;
            return View(GamesVM);
        }
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if(ModelState.IsValid)
            {
                GamesVM.GameDemo.GameID = GamesVM.Game.ID;
                _db.Add(GamesVM.GameDemo);
                var webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    var path = Path.Combine(webRootPath, SD.GameDemoFolder);
                    var extension = Path.GetExtension(files[0].FileName);
                    using(var fileStream = new FileStream(Path.Combine(path,GamesVM.GameDemo.ID + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    GamesVM.GameDemo.Demo = @"\" + SD.GameDemoFolder + @"\" + GamesVM.GameDemo.ID + extension;
                }
                await _db.SaveChangesAsync();
            }              
            return View(GamesVM);
        }
    }
}
