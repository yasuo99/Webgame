using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Extensions;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DichVuGame.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int? id)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if (lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            if (id.HasValue)
            {
                bool alreadyInCart = false;
                var game = await _db.Games.Where(u => u.ID == id).FirstOrDefaultAsync();
                foreach (var item in lstCart)
                {
                    if (item.Game.ID == id)
                    {
                        item.Amount++;
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
            return View(lstCart);
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
        public IActionResult RemoveFromCartInCart(int id)
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingcartSession");
            if(lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            foreach(var item in lstCart)
            {
                if(item.Game.ID == id)
                {
                    lstCart.Remove(item);
                    break;
                }
            }
            HttpContext.Session.Set("ShoppingCartSession", lstCart);
            return View(nameof(Index), lstCart);
        }
    }
}