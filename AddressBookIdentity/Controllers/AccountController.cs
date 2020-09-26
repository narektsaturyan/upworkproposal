using AddressBookIdentity.Models;
using AddressBookIdentity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ContactDbContext db;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                                 ContactDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);

                if(result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(loginViewModel.Email);
                    return RedirectToAction("AddContact", "Home", user.Id);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                    
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser isRegistered = await userManager.FindByNameAsync(registerViewModel.Email);
                if(isRegistered != null)
                {
                    ModelState.AddModelError("", $"User with {registerViewModel.Email} already exists");
                    return View(registerViewModel);
                }
                else
                {
                    var user = new IdentityUser
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email
                    };
                    var result = await userManager.CreateAsync(user, registerViewModel.Password);

                    if(result.Succeeded)
                    { 
                        await signInManager.SignInAsync(user, isPersistent: true);

                        return RedirectToAction("AddContact", "Home", user.Id);
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(registerViewModel);
                    }
                }

            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            
            
                await signInManager.SignOutAsync();
                return RedirectToAction("Login");
         
        }

    }
}
