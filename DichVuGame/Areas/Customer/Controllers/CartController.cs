using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Extensions;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            List<CartViewModel> lstCart = HttpContext.Session.Get<List<CartViewModel>>("ShoppingCartSession");
            if(lstCart == null)
            {
                lstCart = new List<CartViewModel>();
            }
            return View(lstCart);
        }
    }
}