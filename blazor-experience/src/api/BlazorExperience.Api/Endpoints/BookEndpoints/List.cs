using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.BookEndpoints
{
    public class List : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<BookDatatableViewModel>>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public List(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet(Routes.BookService)]
        [SwaggerOperation(
            Summary = "Gets a list of books for dashboard",
            Description = "Gets a list of books for dashboard",
            OperationId = "Book.List",
            Tags = new[] { "BookEndpoints" }
        )]
        public override async Task<ActionResult<List<BookDatatableViewModel>>> HandleAsync(
            CancellationToken cancellationToken)
        {
            var books = _bookService.GetAll();
            return Ok(_mapper.Map<List<BookDatatableViewModel>>(books));
        }
    }
}
