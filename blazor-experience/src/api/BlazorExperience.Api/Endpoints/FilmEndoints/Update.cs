using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels;
using BlazorExperience.Shared.ViewModels.Film;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.FilmEndoints
{
    public class Update : EndpointBaseAsync
        .WithRequest<FilmViewModel>
        .WithActionResult<FilmViewModel>
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public Update(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        [HttpPut(Routes.FilmService)]
        [SwaggerOperation(
            Summary = "Updates a film",
            Description = "Updates a film",
            OperationId = "Films.Update",
            Tags = new[] { "FilmEndpoints" })
        ]
        public override async Task<ActionResult<FilmViewModel>> HandleAsync(FilmViewModel model, CancellationToken cancellationToken = new CancellationToken())
        {
            var book = await _filmService.GetByIdAsync(model.Id);

            if (book == null)
                return NotFound();

            _mapper.Map(model, book);

            _filmService.UpdateAsync(book);

            return Ok(_mapper.Map<FilmViewModel>(book));
        }
    }
}
