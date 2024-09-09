using Moq;
using sampleAPI.Controllers;
using sampleAPI.Models;
using sampleAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

public class ProductsControllerTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _controller = new ProductsController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(GetTestProducts());

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    private List<Product> GetTestProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10 },
            new Product { Id = 2, Name = "Product2", Price = 20 }
        };
    }

    [Fact]
    public async Task GetProductById_ReturnsOkResult_WithProduct()
    {
        // Arrange
        int testProductId = 1;
        var testProduct = new Product { Id = testProductId, Name = "Test Product", Price = 10.0 };
        _mockRepo.Setup(repo => repo.GetProductById(testProductId)).ReturnsAsync(testProduct);

        // Act
        var result = await _controller.GetProduct(testProductId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(testProductId, returnValue.Id);
        Assert.Equal("Test Product", returnValue.Name);
        Assert.Equal(10.0, returnValue.Price);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFoundResult_WhenProductNotFound()
    {
        // Arrange
        int testProductId = 1;
        _mockRepo.Setup(repo => repo.GetProductById(testProductId)).ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(testProductId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostProduct_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var controller = new ProductsController(mockRepository.Object);
        var product = new Product { Id = 1, Name = "Test Product", Price = 10 };

        // Act
        var result = await controller.PostProduct(product);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ProductsController.GetProduct), createdAtActionResult.ActionName);
        Assert.Equal(product.Id, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(product, createdAtActionResult.Value);
        mockRepository.Verify(r => r.AddProduct(product), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContentResult()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var controller = new ProductsController(mockRepository.Object);
        var productId = 1;

        // Act
        var result = await controller.DeleteProduct(productId);

        // Assert
        Assert.IsType<OkResult>(result);
        mockRepository.Verify(r => r.DeleteProduct(productId), Times.Once);
    }

}
