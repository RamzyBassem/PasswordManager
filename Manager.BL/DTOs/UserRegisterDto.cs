using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.DTOs
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
