using AutoMapper;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using Newtonsoft.Json.Linq;

namespace DistributeurBoisson.DAL.Repositories
{
    public class RecetteRepository : IRecetteRepository
    {
        protected IMapper _mapper;

        public RecetteRepository(IMapper mapper)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// Obtient une recette par son nom.
        /// </summary>
        /// <param name="recetteName">Le nom de la recette à rechercher.</param>
        /// <returns>La recette correspondant au nom spécifié.</returns>
        /// <exception cref="InvalidOperationException">Le nom de la recette spécifié n'a pas été trouvé.</exception>
        public Recette GetRecetteByName(string recetteName)
        {
            JToken recetteData = DataJson(Enum.Objects.recettes.ToString(), Enum.Attributes.nom.ToString(), recetteName);

            if(recetteData != null)
            {
                Recette recette = MapRecette(recetteData);
                return recette;
            }
            else
            {
                throw new InvalidOperationException($"La recette avec le nom '{recetteName}' n'a pas été trouvée.");
            }
        }


        /// <summary>
        /// Récupère les données JSON pour un type et un nom spécifiques.
        /// </summary>
        /// <param name="type">Le type de données JSON à récupérer.</param>
        /// <param name="name">Le nom de la propriété à rechercher.</param>
        /// <param name="recetteName">Le nom de la recette à rechercher.</param>
        /// <returns>Les données JSON correspondant à la recette spécifiée.</returns>
        public JToken DataJson(string type, string name, string recetteName)
        {
            JObject donnees = JsonFileReader.ReadJsonFile(GlobalVariable.jsonFilePath);
            JToken recetteData = donnees[type]
                .FirstOrDefault(r => r[name].ToString().Equals(recetteName, StringComparison.OrdinalIgnoreCase));

            return recetteData;
        }


        /// <summary>
        /// Mappe les données JSON d'une recette vers un objet Recette.
        /// </summary>
        /// <param name="recetteData">Les données JSON de la recette à mapper.</param>
        /// <returns>La recette mappée.</returns>
        private static Recette MapRecette(JToken recetteData)
        {
            Recette recette = new Recette
            {
                Nom = recetteData["nom"].ToString(),
                Ingredients = recetteData["ingredients"].Select(ingredient => new RecetteIngredient
                {
                    NomRecette = recetteData["nom"].ToString(),
                    NomIngredient = ingredient["ingredient"].ToString(),
                    Quantite = Convert.ToDouble(ingredient["quantite"])
                }).ToList()
            };
            return recette;
        }
    }
}

