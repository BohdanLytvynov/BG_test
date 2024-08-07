using BookStore.DAL.Persistence;

namespace BookStore.DAL.Repositories.Interfaces
{
    public interface IStreetcodeDbContextProvider
    {
        public BookStoreDbContext BookStoreDb { init; }
    }
}