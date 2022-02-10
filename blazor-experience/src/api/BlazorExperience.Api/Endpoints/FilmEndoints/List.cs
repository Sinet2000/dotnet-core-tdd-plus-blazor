using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorExperience.Services;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels.Film;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorExperience.Api.Endpoints.FilmEndoints
{
    public class List : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<FilmDatatableViewModel>>
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public List(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        [HttpGet(Routes.FilmService)]
        [SwaggerOperation(
            Summary = "Gets a list of films for dashboard",
            Description = "Gets a list of films for dashboard",
            OperationId = "Films.List",
            Tags = new[] { "FilmEndpoints" }
        )]
        public override async Task<ActionResult<List<FilmDatatableViewModel>>> HandleAsync(
            CancellationToken cancellationToken)
        {
            var films = _filmService.GetAll();
            return Ok(_mapper.Map<List<FilmDatatableViewModel>>(films));
        }
    }
}
