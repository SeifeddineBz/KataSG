using DistributeurBoisson.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.DAL.IRepositories
{
    public interface IRecetteRepository
    {
        public List<RecetteIngredient> GetIngredientsByRecetteName(string name);
    }
}
