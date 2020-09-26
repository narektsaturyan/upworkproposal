using AddressBookIdentity.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookIdentity.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ContactDbContext db;

        public AjaxController(ContactDbContext db)
        {
            this.db = db;
        }

        public IActionResult Search(string keyone, string keytwo) 
        {
            var id = HttpContext.User.Identity.Name;
            var htmlResult = String.Empty;
            var aa = HttpContext.Request.GetDisplayUrl();


            if (keyone != null && keytwo == null )
            {
                if(keyone.Length > 0 && keyone.Length < 2)
                {
                    List<Contact> contactList = db.Contacts.Where(c => c.name.StartsWith(keyone) && c.userID == id || c.lastName.StartsWith(keyone) && c.userID == id ||
                                                         c.name.Contains(keyone) && c.userID == id || c.lastName.Contains(keyone) && c.userID == id).ToList();
                    if(contactList.Count == 0)
                    {
                        
                       
                        htmlResult += "<h1>Not Found</h1>";
                    }
                    else
                    {
                        foreach (var item in contactList)
                        {
                            htmlResult += $"<a class=\"btn btn-primary shadow - lg\" style=\"width: 300px; margin: 5px; background-color: darkslategrey; border-color: darkslategrey; cursor: default;\" href=\"/Home/ViewContact/{item.id}\" role=\"button\">{item.name} {item.lastName}</a><br/>";
                        }
                    }
                    
                    return Ok(htmlResult);
                }
                else
                {
                    List<Contact> contactList = db.Contacts.Where(c => c.name.Contains(keyone) && c.userID == id || c.lastName.Contains(keyone) && c.userID == id).ToList();
                    if (contactList.Count == 0)
                    {
                       
                        htmlResult += "<h1>Not Found</h1>";
                    }
                    else
                    {
                        foreach (var item in contactList)
                        {
                            htmlResult += $"<a class=\"btn btn-primary shadow - lg\" style=\"width: 300px; margin: 5px; background-color: darkslategrey; border-color: darkslategrey; cursor: default;\" href=\"/Home/ViewContact/{item.id}\" role=\"button\">{item.name} {item.lastName}</a><br/>";
                        }
                    }
                    
                    return Ok(htmlResult);
                }
                
            }

            if(keyone != null && keytwo != null)
            {
                List<Contact> contactList = db.Contacts.Where(c => c.name.Contains(keyone) && c.lastName.Contains(keytwo) && c.userID == id ||
                                                     c.lastName.Contains(keyone) && c.name.Contains(keytwo) && c.userID == id).ToList();

                if(contactList.Count == 0)
                {
                   
                    htmlResult += "<h1>Not Found</h1>";
                }
                else
                {
                    foreach (var item in contactList)
                    {
                        htmlResult += $"<a class=\"btn btn-primary shadow - lg\" style=\"width: 300px; margin: 5px; background-color: darkslategrey; border-color: darkslategrey; cursor: default;\" href=\"/Home/ViewContact/{item.id}\" role=\"button\">{item.name} {item.lastName}</a><br/>";
                    }
                }
                

                return Ok(htmlResult);
            }
            
            if(keyone == null && keytwo == null)
            {
                List<Contact> contactList = db.Contacts.ToList();
                return Ok(contactList);
            }
            
            return Ok();






        }
    }
}
