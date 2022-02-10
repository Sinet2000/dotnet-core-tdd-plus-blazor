using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.ViewModels.Film;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.FilmEndoints
{
    public class GetById : EndpointBaseAsync
        .WithRequest<long>
        .WithActionResult<FilmViewModel>
    {

        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public GetById(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        [HttpGet(GetFilmByIdRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a single Film",
            Description = "Gets a single Film by Id",
            OperationId = "Films.GetById",
            Tags = new[] { "FilmEndpoints" })
        ]
        public override async Task<ActionResult<FilmViewModel>> HandleAsync([FromRoute] long filmId, CancellationToken cancellationToken)
        {
            var film = await _filmService.GetByIdAsync(filmId);

            return film == null ? NotFound() : Ok(_mapper.Map<FilmViewModel>(film));
        }
    }
}
