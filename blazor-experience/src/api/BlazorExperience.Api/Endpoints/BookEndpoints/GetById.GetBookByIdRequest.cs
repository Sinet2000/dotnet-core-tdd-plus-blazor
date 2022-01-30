using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Endpoints.Book
{
    public class GetBookByIdRequest
    {
        public const string Route = "/Books/{BookId:long}";
        public static string BuildRoute(long bookId) => Route.Replace("{BookId:long}", bookId.ToString());

        public int BookId { get; set; }
    }
}
