using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.DAL.Entities
{
    public class Ingredient
    {
        public string Nom { get; set; } = null!;
        public double PrixParDose { get; set; }
    }
}
