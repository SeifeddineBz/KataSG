using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.BLL.DTO
{
    public class IngredientDto
    {
        public string Nom { get; set; } = null!;
        public double PrixParDose { get; set; }
    }
}
