using AddressBookIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookIdentity.ViewModels
{
    public class AddContactViewModel //Model.contact
    {
        public string search { get; set; }
        public Contact contact { get; set; }
    }
}
