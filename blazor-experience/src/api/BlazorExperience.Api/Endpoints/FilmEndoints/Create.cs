using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels.Film;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.FilmEndoints
{
    public class Create : EndpointBaseAsync
        .WithRequest<FilmViewModel>
        .WithActionResult<FilmViewModel>
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public Create(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        [HttpPost(Routes.FilmService)]
        [SwaggerOperation(
            Summary = "Creates a film",
            Description = "Creates a film",
            OperationId = "Films.Create",
            Tags = new[] { "FilmEndpoints" })
        ]

        public override async Task<ActionResult<FilmViewModel>> HandleAsync(FilmViewModel model, CancellationToken cancellationToken = new CancellationToken())
        {
            var newFilm = _mapper.Map<Core.Models.Film>(model);

            var createdFilm = await _filmService.CreateAsync(newFilm);

            return Ok(_mapper.Map<FilmViewModel>(createdFilm));
        }
    }
}
