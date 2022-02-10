using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BlazorExperience.Core.Models;
using BlazorExperience.Shared.ViewModels;
using BlazorExperience.Shared.ViewModels.Book;
using BlazorExperience.Shared.ViewModels.Film;

namespace BlazorExperience.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TODO: Put AutoMapper mappings here:

            CreateMap<Book, BookViewModel>()
                .ReverseMap();

            CreateMap<Book, BookDatatableViewModel>()
                .ReverseMap();

            CreateMap<Film, FilmViewModel>()
                .ReverseMap();

            CreateMap<Film, FilmDatatableViewModel>()
                .ReverseMap();
        }
    }
}