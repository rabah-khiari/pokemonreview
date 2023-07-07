using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository dbRepository;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository dataContext, IMapper Imapper) 
        {
            dbRepository = dataContext;
            mapper = Imapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            
            var result = dbRepository.GetCategories();
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int id)
        {
            return Ok(dbRepository.GetCategory(id));
        }
        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!dbRepository.CategoryExists(categoryId))
                return NotFound();
            return Ok(dbRepository.GetPokemonByCategory(categoryId));
        }
    }
}
