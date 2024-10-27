using AutoMapper;
using LibrarySystemAPI.Dto;
using LibrarySystemAPI.Models;

namespace LibrarySystemAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
