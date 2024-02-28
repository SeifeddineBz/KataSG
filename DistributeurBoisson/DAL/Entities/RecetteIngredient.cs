using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.DAL.Entities
{
    public class RecetteIngredient
    {
        public string Ingredient { get; set; } = null!;
        public int Quantite { get; set; }
        
    }
}
