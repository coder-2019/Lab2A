//I, Abdullah Mousavi, student number 000069844, certify that this material is my
//original work.No other person's work has been used without due
//acknowledgement and I have not made my work available to anyone else.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1B.Data;
using Lab1B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1B.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> SeedRoles()
        {
            // Add New Users
            ApplicationUser user1 = new ApplicationUser
            {
                Email = "John@doe.com",
                UserName = "John@doe.com",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = "1886-05-11"
            };
            ApplicationUser user2 = new ApplicationUser
            {
                Email = "Jane@doe.com",
                UserName = "Jane@doe.com",
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = "1784-12-08"
            };

            IdentityResult result = await _userManager.CreateAsync(user1, "Mohawk1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _userManager.CreateAsync(user2, "Mohawk1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            // Add Roles
            result = await _roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            result = await _roleManager.CreateAsync(new IdentityRole("Staff"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            // Assign roles to users
            result = await _userManager.AddToRoleAsync(user1, "Manager");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });

            result = await _userManager.AddToRoleAsync(user2, "Staff");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });


            return RedirectToAction("Index", "Home");
        }
    }
}