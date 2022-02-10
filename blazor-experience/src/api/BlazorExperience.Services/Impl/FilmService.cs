using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models;
using BlazorExperience.Data;

namespace BlazorExperience.Services.Impl
{
    public class FilmService : RepositoryBase<Film>, IFilmService
    {
        public FilmService(IDataContext dataContext) : base(dataContext)
        {

        }
    }
}
