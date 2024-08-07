using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? Address { get; set; }

        public DateOnly? BirthDate { get; set; }

        public User()
        {
            
        }
    }
}
