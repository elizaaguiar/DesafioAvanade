using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_User.DTOs
{
    public class UserDTO
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}