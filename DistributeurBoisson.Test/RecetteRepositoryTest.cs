using AutoMapper;
using DistributeurBoisson.BLL.DTO;
using DistributeurBoisson.BLL.Entities;
using DistributeurBoisson.BLL.Service;
using DistributeurBoisson.DAL;
using DistributeurBoisson.DAL.Entities;
using DistributeurBoisson.DAL.IRepositories;
using DistributeurBoisson.DAL.Repositories;
using Moq;
using Newtonsoft.Json.Linq;
using Enum = DistributeurBoisson.DAL.Enum;

namespace DistributeurBoisson.Test
{
    public class RecetteRepositoryTest
    {

        private Mock<IRecetteRepository> mockRepositoryRecette;

        public RecetteRepositoryTest()
        {
            mockRepositoryRecette = new Mock<IRecetteRepository>();
        }

        private IRecetteRepository MakeRecetteRepository()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RecetteIngredient, RecetteIngredientDto>();
            });
            IMapper mapper = new Mapper(mapperConfiguration);

            IRecetteRepository recetteRepository = new RecetteRepository(mapper);

            return recetteRepository;
        }

        private JToken MakeDataJson()
        {
            JToken recetteData = new JObject
            {
                { "nom", "NomDeLaRecette" },
                {
                    "ingredients", new JArray
                    {
                        new JObject { { "ingredient", "Café" }, { "quantite", 1 } },
                        new JObject { { "ingredient", "Eau" }, { "quantite", 0.1 } },
                        new JObject { { "ingredient", "Chocolat" }, { "quantite", 1 } },
                        new JObject { { "ingredient", "Crème" }, { "quantite", 1 } }
                    }
                }
            };

            return recetteData;
        }


        [Fact]
        public void GetRecetteByName_ReturnsRecette()
        {
            // Arrange
            JToken recetteData = MakeDataJson();

            mockRepositoryRecette.Setup(repo => repo.DataJson(Enum.Objects.recettes.ToString(), Enum.Attributes.nom.ToString(), "Cappuccino")).Returns(recetteData);

            // Mocking GetIngredientsByRecetteNameService
            //mockRepositoryRecette.Setup(repo => repo.GetRecetteByName(It.IsAny<string>())).Returns(new Recette
            //{
            //    Ingredients = new List<RecetteIngredient>
            //        {
            //            new RecetteIngredient { NomIngredient = "Café", Quantite = 1 },
            //            new RecetteIngredient { NomIngredient = "Chocolat", Quantite = 1 },
            //            new RecetteIngredient { NomIngredient = "Eau", Quantite = 1 },
            //            new RecetteIngredient { NomIngredient = "Crème", Quantite = 1 }
            //        }
            //});


            // Act
            Recette recette = MakeRecetteRepository().GetRecetteByName("Cappuccino");

            // Assert
            Assert.NotNull(recette);
            Assert.Equal("Cappuccino", recette.Nom);
            Assert.Collection(recette.Ingredients,
                i => { Assert.Equal("Café", i.NomIngredient); Assert.Equal(1, i.Quantite); },
                i => { Assert.Equal("Chocolat", i.NomIngredient); Assert.Equal(1, i.Quantite); },
                i => { Assert.Equal("Eau", i.NomIngredient); Assert.Equal(1, i.Quantite); },
                i => { Assert.Equal("Crème", i.NomIngredient); Assert.Equal(1, i.Quantite); });
        }


        [Fact]
        public void GetRecetteByName_ThrowsExceptionWhenDataJsonFails()
        {
            // Arrange
            mockRepositoryRecette.Setup(repo => repo.DataJson(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<InvalidOperationException>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => MakeRecetteRepository().GetRecetteByName("NomDeLaRecette"));
        }
    }
}