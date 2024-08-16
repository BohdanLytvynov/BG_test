using AutoMapper;
using BookStore.BLL.Dto.Book;
using BookStore.BLL.ValueResolvers;
using BookStore.DAL.Entities;

namespace BookStore.BLL.Mapping.Books
{
    public class BookProfile : Profile
    {

        public BookProfile()
        {
            //CreateMap<Book, BookDto>()
            //    .ForMember(x => x.Authors, conf => conf.MapFrom());
            
        }
    }
}
