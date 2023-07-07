using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository dbRepository;
        private readonly IMapper mapper;
        public PokemonController(IPokemonRepository dataContext, IMapper Imapper)
        {
            dbRepository = dataContext;
            mapper = Imapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            //use automapper
            var pokemon = mapper.Map<List<PokemonDto>>(dbRepository.GetPokemons());
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
                if(!dbRepository.PokemonExists(pokeId))
                    return NotFound();
                return Ok(dbRepository.GetPokemon(pokeId));
            
        }
        [HttpGet("{ratepokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int ratepokeId)
        {
            if (!dbRepository.PokemonExists(ratepokeId))
                return NotFound();
            return Ok(dbRepository.GetPokemonRating(ratepokeId));
        }

        }
}
