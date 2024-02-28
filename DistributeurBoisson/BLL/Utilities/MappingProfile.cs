using AutoMapper;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.Entities;
using DistributeurBoisson.DAL.Entities;

namespace BLL.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecetteIngredient, RecetteIngredientDto>();
            CreateMap<RecetteIngredientDto, RecetteIngredient>();

            CreateMap<Recette, RecetteDto>();
            CreateMap<RecetteDto, Recette>();
        }
    }
}
