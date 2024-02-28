using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace DistributeurBoisson.DAL.Repositories
{
    public class RecetteRepository : IRecetteRepository
    {
        public List<RecetteIngredient> GetIngredientsByRecetteName(string recetteName)
        {
            //JObject donnees = JsonFileReader.ReadJsonFile(jsonFilePath);

            //JToken recetteData = donnees["recettes"].FirstOrDefault(r => r["nom"].ToString().Equals(recetteName, StringComparison.OrdinalIgnoreCase));

            JToken recetteData = DataJson(Enum.Objects.recettes.ToString(), Enum.Attributes.nom.ToString(), recetteName);

            if (recetteData != null)
            {
                return ExtractIngredientsFromRecette(recetteData);
            }
            else
            {
                throw new InvalidOperationException($"La recette avec le nom '{recetteName}' n'a pas été trouvée.");
            }
        }


        private List<RecetteIngredient> ExtractIngredientsFromRecette(JToken recetteData)
        {
            List<RecetteIngredient> ingredients = new List<RecetteIngredient>();

            foreach (JToken ingredient in recetteData["ingredients"])
            {
                string nomIngredient = ingredient["ingredient"].ToString();
                int quantite = Convert.ToInt32(ingredient["quantite"]);

                ingredients.Add(new RecetteIngredient
                {
                    Ingredient = nomIngredient,
                    Quantite = quantite
                });
            }

            return ingredients;
        }


        private JToken DataJson(string type, string name, string recetteName)
        {
            JObject donnees = JsonFileReader.ReadJsonFile(GlobalVariable.jsonFilePath);
            JToken recetteData = donnees[type].FirstOrDefault(r => r[name].ToString().Equals(recetteName, StringComparison.OrdinalIgnoreCase));

            return recetteData;
        }
    }
}

