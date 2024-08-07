using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence
{
    public class BookStoreDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        #region ctor

        #endregion

        #region For Migration

        #endregion
    }
}
