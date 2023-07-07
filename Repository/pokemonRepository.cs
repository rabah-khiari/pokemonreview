using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        public readonly DataContext db;
        public PokemonRepository(DataContext dataContext)
        {
            db = dataContext;
        }

        public Pokemon GetPokemon(int id)
        {
            return db.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return db.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = db.reviews.Where(p => p.Pokemon.Id == pokeId);
           

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            var pokemons = db.Pokemons.OrderBy(p => p.Id).ToList();
            return pokemons;
        }

        public bool PokemonExists(int pokeId)
        {
            return db.Pokemons .Any(p => p.Id == pokeId);
        }
    }

}
