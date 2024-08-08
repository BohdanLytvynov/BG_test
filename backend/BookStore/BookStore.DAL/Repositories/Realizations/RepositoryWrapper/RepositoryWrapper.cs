using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces;
using BookStore.DAL.Repositories.Interfaces.Authors;
using BookStore.DAL.Repositories.Interfaces.Books;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.Authors;
using BookStore.DAL.Repositories.Realizations.Books;

namespace BookStore.DAL.Repositories.Realizations.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IBookRepository _bookRepository;
        
        private IAuthorRepository _authorRepository;

        public IBookRepository BookRepository => GetRepository(_bookRepository as BookRepository);
        
        public IAuthorRepository AuthorRepository { get=> GetRepository(_authorRepository as AuthorRepository); }

        private readonly BookStoreDbContext _db;

        public RepositoryWrapper(BookStoreDbContext db)
        {
            _db = db;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public T GetRepository<T>(T? repo)
     where T : IStreetcodeDbContextProvider, new()
        {
            if (repo is null)
            {
                repo = new T()
                {
                    BookStoreDb = _db
                };
            }

            return repo;
        }
    }
}
