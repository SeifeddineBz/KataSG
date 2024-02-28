using AutoMapper;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.Entities;
using DistributeurBoisson.BLL.IService;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using Enum = DistributeurBoisson.DAL.Enum;

namespace DistributeurBoisson.BLL.Service
{
    public class RecetteService : IRecetteService
    {
        private IRecetteRepository _RepositoryRecette;
        private IIngredientRepository _RepositoryIngredient;
        protected IMapper _mapper;

        public RecetteService(IRecetteRepository repositoryRecette, IIngredientRepository repositoryingredient, IMapper mapper)
        {
            _RepositoryRecette = repositoryRecette;
            _RepositoryIngredient = repositoryingredient;
            _mapper = mapper;
        }


        /// <summary>
        /// Obtient les ingrédients d'une recette spécifique.
        /// </summary>
        /// <param name="recetteName">Le nom de la recette.</param>
        /// <returns>La liste des ingrédients de la recette.</returns>
        public List<RecetteIngredientDto> GetIngredientsByRecetteNameService(string recetteName)
        {
            Recette recetteEntity = _RepositoryRecette.GetRecetteByName(recetteName);
            RecetteDto recette = _mapper.Map<RecetteDto>(recetteEntity);
            List<RecetteIngredientDto> recetteingredients = _mapper.Map<List<RecetteIngredientDto>>(recette.Ingredients);
            return recetteingredients;
        }



        /// <summary>
        /// Calcule le prix d'une recette spécifique.
        /// </summary>
        /// <param name="recetteName">Le nom de la recette.</param>
        /// <returns>Le prix de la recette.</returns>
        public double CalculatePriceRecette(string recetteName)
        {
            List<RecetteIngredientDto> ingredientsWithQuantites = GetIngredientsByRecetteNameService(recetteName);
            List<Ingredient> ingredients = _RepositoryIngredient.GetIngredients(Enum.Objects.ingredients.ToString());

            double price = 0;

            foreach (RecetteIngredientDto ingredient in ingredientsWithQuantites)
            {
                price += ingredients.FirstOrDefault(x => x.Nom == ingredient.NomIngredient).PrixParDose * ingredient.Quantite;
            }

            return price * GlobalVariable.margeBenefice;
        }

    }
}
