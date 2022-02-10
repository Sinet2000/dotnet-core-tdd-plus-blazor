using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.BookEndpoints
{
    public class Create : EndpointBaseAsync
        .WithRequest<BookViewModel>
        .WithActionResult<BookViewModel>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public Create(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpPost(Routes.BookService)]
        [SwaggerOperation(
            Summary = "Creates a book",
            Description = "Creates a book",
            OperationId = "Books.Create",
            Tags = new[] { "BookEndpoints" })
        ]

        public override async Task<ActionResult<BookViewModel>> HandleAsync(BookViewModel model, CancellationToken cancellationToken = new CancellationToken())
        {
            var newBook = _mapper.Map<Core.Models.Book>(model);

            var createdBook = await _bookService.CreateAsync(newBook);

            return Ok(_mapper.Map<BookViewModel>(createdBook));
        }
    }
}
