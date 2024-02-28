

namespace DistributeurBoisson.BLL.DTO
{
    public class RecetteIngredientDto
    {
        public string NomRecette { get; set; } = null!;
        public string NomIngredient { get; set; } = null!;
        public double Quantite { get; set; }

    }
}
