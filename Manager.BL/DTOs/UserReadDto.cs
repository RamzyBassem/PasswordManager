using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.DTOs
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public string Role { get; set; } = "";

        public IEnumerable<IdentityError> ErrorMessage { get; set; }=new List<IdentityError>();
        public bool UserNameExists { get; set; } = false;
    }
}
