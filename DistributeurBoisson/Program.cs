// See https://aka.ms/new-console-template for more information

using AutoMapper;
using BLL.Utilities;
using DistributeurBoisson;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.Entities;
using DistributeurBoisson.BLL.IService;
using DistributeurBoisson.BLL.Service;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using DistributeurBoisson.DAL.Repositories;
using System.Runtime.CompilerServices;


var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<RecetteIngredient, RecetteIngredientDto>();
    cfg.CreateMap<Recette, RecetteDto>();

});
IMapper mapper = new Mapper((IConfigurationProvider)mapperConfiguration);
IRecetteRepository recetteRepository = new RecetteRepository(mapper);
IIngredientRepository ingredientRepository = new IngredientRepository();
IRecetteService recetteService = new RecetteService(recetteRepository, ingredientRepository, mapper);

double ingredients = recetteService.CalculatePriceRecette(GlobalVariable.recetteName);

Console.WriteLine("Le prix de la recette est "+ ingredients.ToString());

