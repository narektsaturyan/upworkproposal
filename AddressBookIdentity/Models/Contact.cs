using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookIdentity.Models
{
    public class Contact
    {
     
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(15)]
        public string phone { get; set; }
        [Required]
        [MaxLength(20)]
        public string name { get; set; }
        [Required]
        [MaxLength(25)]
        public string lastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        public string Address { get; set; }

        public string userID { get; set; }
    }
}
