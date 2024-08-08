using BookStore.DAL.Repositories.Interfaces.Authors;
using BookStore.DAL.Repositories.Interfaces.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        public IBookRepository BookRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
