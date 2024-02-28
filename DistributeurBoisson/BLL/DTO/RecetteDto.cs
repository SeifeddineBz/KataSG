using DistributeurBoisson.BLL.DTO;

namespace DistributeurBoisson.BLL.Entities
{
    public class RecetteDto
    {
        public string Nom { get; set; } = null!;
        public List<RecetteIngredientDto> Ingredients { get; set; } = null!;
    }
}
