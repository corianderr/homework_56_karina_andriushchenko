using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.Models
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}
