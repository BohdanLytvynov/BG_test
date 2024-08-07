using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region For Migration

        #endregion

        #region Db Sets

        public override DbSet<User> Users { get; set; }

        #endregion

        #region Functions

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);
        }

        #endregion
    }
}
