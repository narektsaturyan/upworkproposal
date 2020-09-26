using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AddressBookIdentity.Models
{
    public class ContactDbContext : IdentityDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
