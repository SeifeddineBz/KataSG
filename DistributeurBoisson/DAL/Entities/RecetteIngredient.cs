

namespace DistributeurBoisson.DAL.Entities
{
    public class RecetteIngredient
    {
        public string NomRecette { get; set; } = null!;
        public string NomIngredient { get; set; } = null!;
        public double Quantite { get; set; }
        
    }
}
