
using DichVuGame.Models;
using DichVuGame.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (_db.Roles.Any(r => r.Name == Helper.ADMIN_ROLE)) { 
                if(_db.UserRoles.Any(r => r.RoleId == _db.Roles.Where(u => u.Name == Helper.ADMIN_ROLE).FirstOrDefault().Id))
                {
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        User = "Thanh",
                        PhoneNumber = "0983961054",
                        Address = "Quận 9",
                        Sex = "Nam",
                        EmailConfirmed = true,
                    }, "Admin1@").GetAwaiter().GetResult();
                    ApplicationUser user = _db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, Helper.ADMIN_ROLE);
                }    
            };
            _roleManager.CreateAsync(new IdentityRole(Helper.ADMIN_ROLE)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.CUSTOMERCARE_ROLE)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.MANAGER_ROLE)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.MRHAI_ROLE)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.CUSTOMER_ROLE)).GetAwaiter().GetResult();
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                User = "Thanh",
                PhoneNumber = "0983961054",
                Address = "Quận 9",
                Sex = "Nam",
                EmailConfirmed = true,
            }, "Admin1@").GetAwaiter().GetResult();
            ApplicationUser usertodo = _db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            await _userManager.AddToRoleAsync(usertodo, Helper.ADMIN_ROLE);
        }


    }
}
