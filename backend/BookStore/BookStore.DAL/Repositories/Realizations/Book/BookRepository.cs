using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.Base;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Books
{
    public class BookRepository : RepositoryBase<Book>, IRepositoryBase<Book>
    {
        public BookRepository(BookStoreDbContext context) : base(context) { }

        public BookRepository() : base() { }
        {
            
        }
    }
}
