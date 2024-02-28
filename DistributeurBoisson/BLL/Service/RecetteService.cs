using AutoMapper;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.IService;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<RecetteIngredientDto> GetIngredientsByRecetteNameService(string recetteName)
        {
            List<RecetteIngredient> ingredients = _RepositoryRecette.GetIngredientsByRecetteName(recetteName);
            return _mapper.Map<List<RecetteIngredientDto>>(ingredients);
        }

        public double CalculatePriceRecette(string recetteName)
        {
            List<RecetteIngredientDto> ingredientsWithQuantites = GetIngredientsByRecetteNameService(recetteName);
            List<Ingredient> ingredients = _RepositoryIngredient.GetIngredients();
            //List<IngredientDto> ingredientDtos = _mapper.Map<List<IngredientDto>>(ingredients);
            double price = 0;

            foreach (RecetteIngredientDto ingredient in ingredientsWithQuantites)
            {
                price += ingredients.FirstOrDefault(x => x.Nom == ingredient.Ingredient).PrixParDose * ingredient.Quantite;
            }

            return price * GlobalVariable.margeBenefice;
        }
    }
}
