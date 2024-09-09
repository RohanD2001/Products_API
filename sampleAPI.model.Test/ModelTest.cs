using sampleAPI.Models;

namespace sampleAPI.model.Test
{
    public class ModelTest
    {
      
        [Fact]
        public void Product_DefaultValues()
        {
            // Arrange & Act
            var product = new Product();

            // Assert
            Assert.Equal(0, product.Id);
            Assert.Null(product.Name);
            Assert.Equal(0.0, product.Price);
        }

        [Fact]
        public void Product_PropertyAssignments()
        {
            // Arrange
            var product = new Product();
            var id = 1;
            var name = "Test Product";
            var price = 99.99;

            // Act
            product.Id = id;
            product.Name = name;
            product.Price = price;

            // Assert
            Assert.Equal(id, product.Id);
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
        }
    }
}