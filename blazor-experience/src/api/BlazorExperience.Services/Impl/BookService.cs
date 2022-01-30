using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models;
using BlazorExperience.Data;

namespace BlazorExperience.Services.Impl
{
    public class BookService : RepositoryBase<Book>, IBookService
    {
        public BookService(IDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
