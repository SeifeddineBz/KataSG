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
        private IRecetteRepository _RepositoryRecette;

        public IngredientRepository(IRecetteRepository repositoryRecette)
        {
            _RepositoryRecette = repositoryRecette;
        }
        
        //Retourne la liste des ingrédients (Nom est prix) pour une recette
        public List<Ingredient> GetIngredients()
        {
            List<string> listIngrediantsName = GetIngrdientsNamesFromRecette();
            List<Ingredient> ingrediants = DataJson(Enum.Objects.ingredients.ToString()).Where(ingredient => listIngrediantsName.Contains(ingredient.Nom)).ToList();

            return ingrediants;
        }


        private List<string> GetIngrdientsNamesFromRecette()
        {
            List<RecetteIngredient> recetteIngredients = _RepositoryRecette.GetIngredientsByRecetteName(GlobalVariable.recetteName);
            List<string> IngredientsList = recetteIngredients.Select(x => x.Ingredient).ToList();

            return IngredientsList;
        }

        private List<Ingredient> DataJson(string type)
        {
            JObject donnees = JsonFileReader.ReadJsonFile(GlobalVariable.jsonFilePath);
            return ConvertToListIngredients(donnees[type]);
        }

        private List<Ingredient> ConvertToListIngredients(JToken ingrediantsToConvert)
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
