using DistributeurBoisson.DAL.Entities;

namespace DistributeurBoisson.DAL.IRepositories
{
    public interface IIngredientRepository
    {
        public List<Ingredient> GetIngredients();
    }
}
