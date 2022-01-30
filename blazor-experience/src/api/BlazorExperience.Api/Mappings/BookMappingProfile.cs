using BlazorExperience.Core.Models;
using BlazorExperience.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Mappings
{
    public class BookMappingProfile : AutoMapper.Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
        }
    }
}
