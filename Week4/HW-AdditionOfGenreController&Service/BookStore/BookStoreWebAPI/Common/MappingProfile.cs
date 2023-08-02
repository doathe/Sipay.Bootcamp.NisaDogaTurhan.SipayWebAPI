﻿using AutoMapper;
using BookStoreWebAPI.Application.BookOperations.Commands.CreateBook;
using BookStoreWebAPI.Application.BookOperations.Queries.GetBookDetail;
using BookStoreWebAPI.Application.BookOperations.Queries.GetBooks;
using BookStoreWebAPI.Application.GenreOperations.Commands.CreateGenre;
using BookStoreWebAPI.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreWebAPI.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book Model Mapping

        // Maps CreateBookModel to Book
        CreateMap<CreateBookModel, Book>();

        // Maps Book to BookDetailViewModel
        CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yy")));

        // Maps Book to BooksViewModel
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => (src.PublishDate.Date.ToString("dd/MM/yy"))));

        // Genre Model Mapping

        // Maps Genre to GenresViewModel
        CreateMap<Genre, GenresViewModel>();

        // Maps Genre to GenreDetailViewModel
        CreateMap<Genre, GenreDetailViewModel>();

        // Maps CreateGenreCommand to Genre
        CreateMap<CreateGenreCommand, Genre>();
    }
}
