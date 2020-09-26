using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace ZevitAddressBook.ViewComponents
{
    public class ContactListViewComponent : ViewComponent
    {
        private ContactDbContext db;
        private readonly UserManager<IdentityUser> manager;

        public ContactListViewComponent(ContactDbContext context, UserManager<IdentityUser> manager)
        {
            db = context;
            this.manager = manager;
        }
        
        
        public async Task<IViewComponentResult> InvokeAsync(string text)
        {
            string currentUserName = HttpContext.User.Identity.Name;
           
           
            if(text == null)
            {
                var result = await db.Contacts.Where(contact => contact.userID == currentUserName).ToListAsync();
                return View(result);
            }

            else
            {
                return View();
            }
            
            
        }

        
    }
}
