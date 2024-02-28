

namespace DistributeurBoisson.DAL.Entities
{
    public class Recette
    {
        public string Nom { get; set; } = null!;
        public List<RecetteIngredient> Ingredients { get; set; } = null!;
    }
}
