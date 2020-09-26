using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressBookIdentity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using AddressBookIdentity.ViewModels;

namespace AddressBookIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        private readonly ContactDbContext db;
       

        public HomeController(ContactDbContext context)
        {
            db = context;
           
        }

       
        [Route("Error/{statusCode}")]
        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            
              return View("ErrorView", statusCode);
           
        }


        public async Task<IActionResult> AddContact()
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            HttpClient hc = new HttpClient();
            var response = await hc.GetStringAsync($"http://ip-api.com/json/{ip}");
            IpApiJsonResult result = JsonConvert.DeserializeObject<IpApiJsonResult>(response);
            if (result.status == "fail")
            {
                ViewBag.Country = "Location: " + "Somewhere in Milky Way";
            }
            else
            {
                ViewBag.Country = "Location: " + result.country;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactViewModel addContact)
        {
            addContact.contact.userID = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
                db.Contacts.Add(addContact.contact);
            await db.SaveChangesAsync();
            return RedirectToAction("AddContact");


        }

        public async Task<IActionResult> ViewContact(int? id)
        {
            Contact contact = await db.Contacts.FirstOrDefaultAsync(c => c.id == id);
            if (contact != null)
                return View(contact);
            return RedirectToAction("AddContact");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Contact user = await db.Contacts.FirstOrDefaultAsync(p => p.id == id);
                if (user != null)
                {
                    db.Contacts.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("AddContact");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditContact(int? id)
        {

            Contact contact = await db.Contacts.FirstOrDefaultAsync(c => c.id == id);
            return View(contact);


        }

        [HttpPost]
        public async Task<IActionResult> EditContact(Contact contact, int? id)
        {
            contact.userID = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Contacts.Update(contact);
                await db.SaveChangesAsync();
                return await ViewContact(id);

            }

            return await ViewContact(id);
        }

    }
}
