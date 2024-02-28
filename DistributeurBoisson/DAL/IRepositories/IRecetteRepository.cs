using DistributeurBoisson.DAL.Entities;
using Newtonsoft.Json.Linq;


namespace DistributeurBoisson.DAL.IRepositories
{
    public interface IRecetteRepository
    {
        public Recette GetRecetteByName(string recetteName);
        public JToken DataJson(string type, string name, string recetteName);
    }
}
