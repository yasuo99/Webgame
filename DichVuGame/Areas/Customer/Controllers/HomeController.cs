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
using DichVuGame.Utility;

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
                ApplicationUser = new ApplicationUser(),
                Comments = new List<Comment>(),
                Reviews = new List<Review>(),
            };
        }

        public async Task<IActionResult> Index()
        {
            var user = await _db.ApplicationUsers.Where(u => u.Email == User.Identity.Name).FirstOrDefaultAsync();
            if(user != null)
            {
                if(User.IsInRole(Helper.ADMIN_ROLE) || User.IsInRole(Helper.CUSTOMERCARE_ROLE) || User.IsInRole(Helper.MANAGER_ROLE) || User.IsInRole(Helper.MRHAI_ROLE))
                {
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                }
            }    
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            List<CartViewModel> lstCart2 = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession2");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            if (lstCart2 == null)
            {
                lstCart2 = new List<CartViewModel>();
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            HttpContext.Session.Set("ShoppingCartSession2", lstCart2);
            var games = await _db.Games.Include(u => u.Studio).ToListAsync();
            gamesVM.Games = games;
            return View(gamesVM);
        }
        public async Task<IActionResult> Search(string q = null)
        {
            if (q != null)
            {
                var queryGames = await _db.Games.Where(u => u.Gamename.ToLower().Trim().Contains(q.ToLower().Trim())).ToListAsync();
                gamesVM.Games = queryGames;
                return View(nameof(Index), gamesVM);

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id, string requireLogin = null,string outOfRange = null)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            List<CartViewModel> lstCart2 = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession2");
            if (lstCart2 == null)
            {
                lstCart2 = new List<CartViewModel>();
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            HttpContext.Session.Set("ShoppingCartSession2", lstCart2);
            var game = await _db.Games.Where(u => u.ID == id).Include(u => u.Studio).FirstOrDefaultAsync();
            var user = await _db.ApplicationUsers.Where(u => u.Email == User.Identity.Name).FirstOrDefaultAsync();
            var commentOfGame = await (from a in _db.Games
                                       join b in _db.GameComments
                                       on a.ID equals b.GameID
                                       join c in _db.Comments on b.CommentID equals c.ID
                                       where a.ID == id
                                       select c).Include(u => u.ApplicationUser).ToListAsync();
            var reviewOfGame = await (from a in _db.Games
                                      join b in _db.GameReviews
                                      on a.ID equals b.GameID
                                      join c in _db.Reviews on b.ReviewID equals c.ID
                                      where a.ID == id
                                      select c).Include(u => u.ApplicationUser).ToListAsync();
            if (user != null)
            {
                var bought = await (from a in _db.Orders
                                    join b in _db.OrderDetails
                                    on a.ID equals b.OrderID
                                    where a.ApplicationUserID == user.Id
                                    && b.Code.GameID == id
                                    select b.Code.Game).FirstOrDefaultAsync();
                if (bought != null)
                {
                    gamesVM.WasBought = true;
                }
            }
            gamesVM.Game = game;
            gamesVM.ApplicationUser = user;
            gamesVM.Comments = commentOfGame.OrderByDescending(u => u.CommentDate).ToList();
            gamesVM.Reviews = reviewOfGame.OrderByDescending(u => u.Vote).ToList();
            if (requireLogin != null)
            {
                ViewBag.RequireLogin = "Vui lòng đăng nhập để bình luận";
            }
            if(outOfRange != null)
            {
                ViewBag.OutOfRange = "Vui lòng chọn lại số lượng sản phẩm";
            }
            return View(gamesVM);
        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPOST(int id, string type,int time)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            List<CartViewModel> lstCart2 = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession2");
            if (lstCart2 == null)
            {
                lstCart2 = new List<CartViewModel>();
            }
            if (type == "Code")
            {
                bool alreadyInCart = false;
                foreach (var item in lstCart)
                {
                    if (item.Game.ID == id)
                    {
                        var temp = item.Amount += gamesVM.Amount;
                        if (temp <= item.Game.AvailableCode)
                        {
                            item.Amount += gamesVM.Amount;
                        }
                        alreadyInCart = true;
                        break;
                    }
                }
                if (!alreadyInCart)
                {
                    var game = _db.Games.Where(u => u.ID == id).FirstOrDefault();
                    if(gamesVM.Amount <= game.AvailableCode)
                    {
                        lstCart.Add(new CartViewModel()
                        {
                            Game = game,
                            Amount = gamesVM.Amount
                        });
                    }
                    else
                    {
                        return RedirectToAction("Details", "Home", new { id = id,outOfRange = "1" });
                    }
                }
            }
            if (type == "Account")
            {
                bool alreadyInCart = false;
                foreach (var item in lstCart2)
                {
                    if (item.Game.ID == id)
                    {
                        item.Amount += time;
                        alreadyInCart = true;
                        break;
                    }
                }
                if (!alreadyInCart)
                {
                    var game = _db.Games.Where(u => u.ID == id).FirstOrDefault();
                    lstCart2.Add(new CartViewModel()
                    {
                        Game = game,
                        Amount = time
                    });
                }
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            HttpContext.Session.Set("ShoppingCartSession2", lstCart2);
            return RedirectToAction("Details", "Home", new { id = id });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SpinWheel()
        {
            return View();
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            bool alreadyInCart = false;
            var game = _db.Games.Where(u => u.ID == id).FirstOrDefault();
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            foreach (var item in lstCart)
            {
                if (item.Game.ID == id)
                {
                    item.Amount++;
                    alreadyInCart = true;
                    break;
                }
            }
            if (!alreadyInCart)
            {
                lstCart.Add(new CartViewModel()
                {
                    Game = game,
                    Amount = 1
                });
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            ViewBag.AddToCart = "Thành công: Bạn đã thêm " + game.Gamename + " vào giỏ hàng";
            var games = await _db.Games.Include(u => u.Studio).ToListAsync();
            gamesVM.Games = games;
            return View(nameof(Index), gamesVM);
        }
        //public void CheckTimeOut()
        //{
        //    var rentingAccount = _db.GameAccounts.Where(ga => ga.Available == false).ToList();
        //    if (rentingAccount != null)
        //    {
        //        while (rentingAccount.Count > 0)
        //        {
        //            foreach (var x in rentingAccount)
        //            {
        //                var onGoing = _db.RentalHistories.Where(r => r.GameAccountID == x.ID && r.OnGoing == true).FirstOrDefault();
        //                if (DateTime.Compare(DateTime.Now, onGoing.EndRenting) >= 0)
        //                {
        //                    var gameAccount = _db.GameAccounts.Where(g => g.ID == onGoing.GameAccountID).FirstOrDefault();
        //                    gameAccount.Password = RandomString(10);
        //                    gameAccount.Available = true;
        //                    _db.GameAccounts.Update(gameAccount);
        //                    onGoing.OnGoing = false;
        //                    _db.SaveChanges();
        //                }
        //            }
        //        }
        //    }

        //}
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
