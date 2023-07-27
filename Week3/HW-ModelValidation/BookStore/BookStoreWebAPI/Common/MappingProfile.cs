using AutoMapper;
using BookStoreWebAPI.BookOperations.CreateBook;
using BookStoreWebAPI.BookOperations.GetBookDetail;
using BookStoreWebAPI.BookOperations.GetBooks;
using BookStoreWebAPI.BookOperations.UpdateBook;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Maps CreateBookModel to Book
        CreateMap<CreateBookModel, Book>();

        // Maps Book to BookDetailViewModel
        CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => (src.PublishDate.Date.ToString("dd/MM/yy"))));

        // Maps Book to BooksViewModel
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => (src.PublishDate.Date.ToString("dd/MM/yy"))));
    }
}
