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
    public class IngredientRepositoryTest
    {

        private Mock<IIngredientRepository> mockRepositoryIngredient;

        public IngredientRepositoryTest()
        {
            mockRepositoryIngredient = new Mock<IIngredientRepository>();
        }

        private IIngredientRepository MakeIngredientRepository()
        {
            IIngredientRepository ingredientRepository = new IngredientRepository();

            return ingredientRepository;
        }

        private List<Ingredient> MakeDataJson()
        {
            List<Ingredient> ingredientData = new List<Ingredient>
            {
                new Ingredient{ Nom = "Café", PrixParDose = 1 },
                new Ingredient{ Nom = "Sucre", PrixParDose = 0.1 },
                new Ingredient{ Nom = "Crème", PrixParDose = 0.5 },
                new Ingredient{ Nom = "Thé", PrixParDose = 2 },
                new Ingredient{ Nom = "Eau", PrixParDose = 0.2 },
                new Ingredient{ Nom = "Chocolat", PrixParDose = 1 },
                new Ingredient{ Nom = "Lait", PrixParDose = 0.4 }
            };

            return ingredientData;
        }



        [Fact]
        public void GetIngredients_ReturnsIngredientsList()
        {
            // Arrange
            List<Ingredient> ingredientsData = MakeDataJson();
            mockRepositoryIngredient.Setup(repo => repo.DataJson(Enum.Objects.ingredients.ToString())).Returns(ingredientsData);

            // Act
            List<Ingredient> ingredients = MakeIngredientRepository().GetIngredients(Enum.Objects.ingredients.ToString());

            // Assert
            Assert.NotNull(ingredients);
            Assert.Equal(ingredientsData.Count(), ingredients.Count);
            Assert.Collection(ingredients,
                i => { Assert.Equal("Café", i.Nom); Assert.Equal(1.0, i.PrixParDose); },
                i => { Assert.Equal("Sucre", i.Nom); Assert.Equal(0.1, i.PrixParDose); },
                i => { Assert.Equal("Crème", i.Nom); Assert.Equal(0.5, i.PrixParDose); },
                i => { Assert.Equal("Thé", i.Nom); Assert.Equal(2.0, i.PrixParDose); },
                i => { Assert.Equal("Eau", i.Nom); Assert.Equal(0.2, i.PrixParDose); },
                i => { Assert.Equal("Chocolat", i.Nom); Assert.Equal(1.0, i.PrixParDose); },
                i => { Assert.Equal("Lait", i.Nom); Assert.Equal(0.4, i.PrixParDose); });
        }


        [Fact]
        public void GetIngredients_ThrowsExceptionWhenDataJsonFails()
        {
            // Arrange
            mockRepositoryIngredient.Setup(repo => repo.DataJson(It.IsAny<string>())).Throws<InvalidOperationException>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => MakeIngredientRepository().GetIngredients("DataErronée"));
        }

    }
}