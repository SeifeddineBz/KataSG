using AutoMapper;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DistributeurBoisson.DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {


        /// <summary>
        /// Obtient la liste des ingrédients d'un certain type.
        /// </summary>
        /// <param name="type">Le type d'ingrédients à récupérer.</param>
        /// <returns>La liste des ingrédients du type spécifié.</returns>
        public List<Ingredient> GetIngredients(string type)
        {
            List<Ingredient> ingrediants = DataJson(type);

            return ingrediants;
        }



        /// <summary>
        /// Récupère les données JSON pour un type spécifique et les convertit en une liste d'ingrédients.
        /// </summary>
        /// <param name="type">Le type de données JSON à récupérer.</param>
        /// <returns>La liste des ingrédients obtenue à partir des données JSON.</returns>
        /// <exception cref="InvalidOperationException">Le type spécifié ne contient aucun ingrédient.</exception>
        public List<Ingredient> DataJson(string type)
        {
            JObject donnees = JsonFileReader.ReadJsonFile(GlobalVariable.jsonFilePath);
            if(donnees[type] != null)
            {
                return ConvertToListIngredients(donnees[type]);
            }
            else
            {
                throw new InvalidOperationException($"aucun ingrédient trouvé !.");
            }
            
        }


        /// <summary>
        /// Convertit les données JSON des ingrédients en une liste d'objets Ingredient.
        /// </summary>
        /// <param name="ingrediantsToConvert">Les données JSON à convertir.</param>
        /// <returns>La liste des ingrédients convertie.</returns>
        private static List<Ingredient> ConvertToListIngredients(JToken ingrediantsToConvert)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();

            foreach (JToken ingredientToken in ingrediantsToConvert)
            {
                Ingredient ingredient = new Ingredient
                {
                    Nom = ingredientToken["nom"].ToString(),
                    PrixParDose = ingredientToken["prixParDose"].ToObject<double>()
                };

                ingredientsList.Add(ingredient);
            }

            return ingredientsList;
        }

    }

}
