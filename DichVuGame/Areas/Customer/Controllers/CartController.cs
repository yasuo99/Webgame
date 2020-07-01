using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Extensions;
using DichVuGame.Models;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PayPal;

namespace DichVuGame.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private ApplicationDbContext _db;
        [BindProperty]
        public SuperCartViewModel SuperCartViewModel { get; set; }
        public CartController(ApplicationDbContext db)
        {
            _db = db;
            SuperCartViewModel = new SuperCartViewModel()
            {
                CartVM1 = new List<CartViewModel>(),
                CartVM2 = new List<CartViewModel>(),
                AccountDiscount = false,
                GameDiscount = false
            };
        }
        public async Task<IActionResult> Index(int? id)
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
            if (id.HasValue)
            {
                bool alreadyInCart = false;
                var game = await _db.Games.Where(u => u.ID == id).FirstOrDefaultAsync();
                foreach (var item in lstCart)
                {
                    if (item.Game.ID == id)
                    {
                        if (item.Amount < item.Game.AvailableCode)
                        {
                            item.Amount++;
                        }
                        alreadyInCart = true;
                        break;
                    }
                }
                if (alreadyInCart == false)
                {
                    lstCart.Add(new CartViewModel()
                    {
                        Game = game,
                        Amount = 1,
                    });
                }
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            HttpContext.Session.Set("ShoppingCartSession2", lstCart2);
            List<CartViewModel> cartViewModels = new List<CartViewModel>();
            List<CartViewModel> cartViewModels1 = new List<CartViewModel>();
            foreach (var product in lstCart)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels.Add(product);
            }
            foreach (var product in lstCart2)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels1.Add(product);
            }
            SuperCartViewModel cartViewModel = new SuperCartViewModel()
            {
                CartVM1 = cartViewModels,
                CartVM2 = cartViewModels1,
                AccountDiscount = false,
                GameDiscount = false
            };
            return View(cartViewModel);
        }
        public IActionResult RemoveFromCartInHome(int id)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            foreach (var item in lstCart)
            {
                if (item.Game.ID == id)
                {
                    lstCart.Remove(item);
                    break;
                }
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> RemoveFromCartInCart(int id, string type = null)
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
                foreach (var item in lstCart)
                {
                    if (item.Game.ID == id)
                    {
                        lstCart.Remove(item);
                        break;
                    }
                }
            }
            if (type == "Account")
            {
                foreach (var item in lstCart2)
                {
                    if (item.Game.ID == id)
                    {
                        lstCart2.Remove(item);
                        break;
                    }
                }
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            HttpContext.Session.Set("ShoppingCartSession2", lstCart2);
            List<CartViewModel> cartViewModels = new List<CartViewModel>();
            List<CartViewModel> cartViewModels1 = new List<CartViewModel>();
            foreach (var product in lstCart)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels.Add(product);
            }
            foreach (var product in lstCart2)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels1.Add(product);
            }
            SuperCartViewModel cartViewModel = new SuperCartViewModel()
            {
                CartVM1 = cartViewModels,
                CartVM2 = cartViewModels1
            };
            return View(nameof(Index), cartViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string Discount = null)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            List<CartViewModel> lstCart2 = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession2");
            List<CartViewModel> cartViewModels = new List<CartViewModel>();
            List<CartViewModel> cartViewModels1 = new List<CartViewModel>();
            foreach (var product in lstCart)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels.Add(product);
            }
            foreach (var product in lstCart2)
            {
                var studio = await _db.Studios.Where(u => u.ID == product.Game.StudioID).FirstOrDefaultAsync();
                product.Studio = studio;
                cartViewModels1.Add(product);
            }
            SuperCartViewModel cartViewModel = new SuperCartViewModel()
            {
                CartVM1 = cartViewModels,
                CartVM2 = cartViewModels1
            };
            var user = await _db.ApplicationUsers.Where(u => u.Email == User.Identity.Name).FirstOrDefaultAsync();
            var sum1 = lstCart.Sum(u => u.Amount * u.Game.Price);
            var sum2 = lstCart.Sum(u => u.Amount * (u.Game.Price * 0.1));
            if (user.Balance >= (sum1 + sum2))
            {
                Order order = new Order()
                {
                    ApplicationUserID = user.Id,
                    Total = sum1,
                    PurchasedDate = DateTime.Now,
                };
                _db.Add(order);
                _db.SaveChanges();
                foreach (var product in lstCart)
                {
                    var code = await _db.Codes.Where(u => u.GameID == product.Game.ID && u.Available == true).FirstOrDefaultAsync();
                    code.Available = false;
                    var game = await _db.Games.Where(u => u.ID == product.Game.ID).FirstOrDefaultAsync();
                    game.AvailableCode -= 1;
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderID = order.ID,
                        CodeID = code.ID
                    };
                    _db.Add(orderDetail);
                    _db.SaveChanges();
                }
                RentalHistory rentalHistory = new RentalHistory()
                {
                    ApplicationUserID = user.Id,
                    Total = sum2
                };
                _db.Add(rentalHistory);
                _db.SaveChanges();
                foreach (var product in lstCart2)
                {
                    var gameAccount = await _db.GameAccounts.Where(u => u.GameID == product.Game.ID && u.Available == true).FirstOrDefaultAsync();
                    gameAccount.Available = false;
                    var game = await _db.Games.Where(u => u.ID == product.Game.ID).FirstOrDefaultAsync();
                    game.AvailableAccount -= 1;
                    RentalDetail rentalDetail = new RentalDetail()
                    {
                        RentalHistoryID = rentalHistory.ID,
                        GameAccountID = gameAccount.ID,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddHours(product.Amount),
                        OnGoing = true
                    };
                    _db.Add(rentalDetail);
                    _db.SaveChanges();

                }
                user.Balance -= (sum1 + sum2);
                await _db.SaveChangesAsync();
            }
            cartViewModel.Total = sum1 + sum2;
            return View(cartViewModel);
        }
    }
}