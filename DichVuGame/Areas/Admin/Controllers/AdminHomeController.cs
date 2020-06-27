using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DichVuGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ApplicationUserViewModel UsersVM { get; set; }
        public AdminHomeController(ApplicationDbContext db)
        {
            _db = db;
            UsersVM = new ApplicationUserViewModel()
            {
                ApplicationUser = new Models.ApplicationUser(),
                ApplicationUsers = new List<Models.ApplicationUser>()
            };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
