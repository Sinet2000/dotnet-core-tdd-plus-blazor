using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.BookEndpoints
{
    public class Update : EndpointBaseAsync
        .WithRequest<BookViewModel>
        .WithActionResult<BookViewModel>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public Update(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpPut(Routes.BookService)]
        [SwaggerOperation(
            Summary = "Updates a book",
            Description = "Updates a book",
            OperationId = "Books.Update",
            Tags = new[] { "BookEndpoints" })
        ]
        public override async Task<ActionResult<BookViewModel>> HandleAsync(BookViewModel model, CancellationToken cancellationToken = new CancellationToken())
        {
            var book = await _bookService.GetByIdAsync(model.Id);

            if (book == null)
                return NotFound();

            _mapper.Map(model, book);

            _bookService.UpdateAsync(book);

            return Ok(_mapper.Map<BookViewModel>(book));
        }
    }
}