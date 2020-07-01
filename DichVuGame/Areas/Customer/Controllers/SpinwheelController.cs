using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DichVuGame.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SpinwheelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
