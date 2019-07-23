using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcmmOja.Utility
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Citizenship { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
