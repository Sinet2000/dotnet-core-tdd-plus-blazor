using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Endpoints.Book
{
    public class GetById : EndpointBaseAsync
        .WithRequest<GetBookByIdRequest>
        .WithActionResult<BookViewModel>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public GetById(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet(GetBookByIdRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a single Book",
            Description = "Gets a single Book by Id",
            OperationId = "Books.GetById",
            Tags = new[] { "BookEndpoints"}) 
        ]
        public override async Task<ActionResult<BookViewModel>> HandleAsync([FromRoute] GetBookByIdRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.BookId);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookViewModel>(book));
        }
    }
}
