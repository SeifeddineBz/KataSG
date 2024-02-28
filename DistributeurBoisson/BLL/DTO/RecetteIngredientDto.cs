using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.BLL.DTO
{
    public class RecetteIngredientDto
    {
        public string Ingredient { get; set; } = null!;
        public int Quantite { get; set; }
        
    }
}
