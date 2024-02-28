// See https://aka.ms/new-console-template for more information

using AutoMapper;
using DistributeurBoisson;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.IService;
using DistributeurBoisson.BLL.Service;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using DistributeurBoisson.DAL.Repositories;
using System.Runtime.CompilerServices;

// Setup dependencies
IRecetteRepository recetteRepository = new RecetteRepository();
IIngredientRepository ingredientRepository = new IngredientRepository(recetteRepository);
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<RecetteIngredient, RecetteIngredientDto>();
    // Add any other mappings if needed
});
IMapper mapper = new Mapper(mapperConfiguration);

// Create RecetteService instance with dependencies
IRecetteService recetteService = new RecetteService(recetteRepository, ingredientRepository, mapper);

// Now you can use recetteService
ingredientRepository.GetIngredients();
double ingredients = recetteService.CalculatePriceRecette(GlobalVariable.recetteName);

Console.WriteLine("Le prix de la recette est "+ ingredients.ToString());

