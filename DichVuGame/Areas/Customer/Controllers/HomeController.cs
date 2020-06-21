using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DichVuGame.Models;
using DichVuGame.Data;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using DichVuGame.Extensions;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using DichVuGame.Helpers;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace DichVuGame.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        [BindProperty]
        public GamesViewModel gamesVM { get; set; }
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            gamesVM = new GamesViewModel()
            {
                Game = new Game(),
                Games = new List<Game>(),
                GameTags = new List<GameTag>(),
                GameDemos = new List<GameDemo>()
            };
        }

        public async Task<IActionResult> Index()
        {            
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            var games = await _db.Games.Include(u => u.Studio).ToListAsync();
            gamesVM.Games = games;
            return View(gamesVM);
        }
        public async Task<IActionResult> Search(string q = null)
        {
            if(q != null)
            {
                var queryGames = await _db.Games.Where(u => u.Gamename.ToLower().Trim().Contains(q.ToLower().Trim())).ToListAsync();
                gamesVM.Games = queryGames;
                return View(nameof(Index),gamesVM);

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            var game = await _db.Games.Where(u => u.ID == id).Include(u =>u.Studio).FirstOrDefaultAsync();
            gamesVM.Game = game;
            return View(gamesVM);
        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPOST(int id)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            bool alreadyInCart = false;
            foreach(var item in lstCart)
            {
                if(item.Game.ID == id)
                {
                    item.Amount += gamesVM.Amount;
                    alreadyInCart = true;
                    break;
                }
            }
            if (!alreadyInCart)
            {
                var game = _db.Games.Where(u => u.ID == id).FirstOrDefault();
                lstCart.Add(new CartViewModel()
                {
                    Game = game,
                    Amount = gamesVM.Amount
                });
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            return RedirectToAction("Details", "Home", new { id = id });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            bool alreadyInCart = false;
            var game = _db.Games.Where(u => u.ID == id).FirstOrDefault();
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if(lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            foreach(var item in lstCart)
            {
                if(item.Game.ID == id)
                {
                    item.Amount++;
                    alreadyInCart = true;
                    break;
                }
            }
            if(!alreadyInCart)
            {
                lstCart.Add(new CartViewModel()
                {
                    Game = game,
                    Amount = 1
                });
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            ViewBag.AddToCart = "Thành công: Bạn đã thêm " +game.Gamename + " vào giỏ hàng";
            var games = await _db.Games.Include(u => u.Studio).ToListAsync();
            gamesVM.Games = games;
            return View(nameof(Index),gamesVM);
        }
        public void CheckTimeOut()
        {
            var rentingAccount = _db.GameAccounts.Where(ga => ga.Available == false).ToList();
            if(rentingAccount != null)
            {
                while (rentingAccount.Count > 0)
                {
                    foreach (var x in rentingAccount)
                    {
                        var onGoing = _db.RentalHistories.Where(r => r.GameAccountID == x.ID && r.OnGoing == true).FirstOrDefault();
                        if (DateTime.Compare(DateTime.Now, onGoing.EndRenting) >= 0)
                        {
                            var gameAccount = _db.GameAccounts.Where(g => g.ID == onGoing.GameAccountID).FirstOrDefault();
                            gameAccount.Password = RandomString(10);
                            gameAccount.Available = true;
                            _db.GameAccounts.Update(gameAccount);
                            onGoing.OnGoing = false;
                            _db.SaveChanges();
                        }
                    }
                }
            }    

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
