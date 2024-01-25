using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Moq;
using Products.Application.Query.Product;
using Products.Application.QueryHandler.Product;
using Products.DAL.Contract;
using Products.DAL.Entity;
using Products.Model.Dto;

namespace Products.Test;

public class ProductsUnitTest
{
    private IEnumerable<ProductListDto> ProductsList => new List<ProductListDto>() {new ProductListDto
            {
                Name = "Book",
                ColourName = "Purple"
            },
            new ProductListDto
            {
                Name = "Map",
                ColourName = "Purple"
            },
            new ProductListDto
            {
                Name = "Car",
                ColourName = "Brown"
            }};

    [Fact]
    public async void All_Products_Can_Be_Returned()
    {
        //Arrange
        var mockRepository = new Mock<IRepository<Product, ProductDetailDto, ProductListDto>>();
        mockRepository.Setup(m => m.ListAsync()).ReturnsAsync(ProductsList);

        var mediator = new Mock<IMediator>();

        ListAllProducts qry = new ListAllProducts();
        ListAllProductsHandler handler = new ListAllProductsHandler(mockRepository.Object);

        //Act
        IEnumerable<ProductListDto> result = await handler.Handle(qry, new System.Threading.CancellationToken());

        //assert
        Assert.Equal(ProductsList.Count(), result.Count());
    }

    [Fact]
    public async void Coloured_Products_Can_Be_Returned()
    {
        //Arrange
        const string COLOUR_NAME = "Brown";

        var mockRepository = new Mock<IRepository<Product, ProductDetailDto, ProductListDto>>();
        mockRepository.Setup(m => m.ListWhereAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(
            ProductsList.Where(x => x.ColourName.Equals(COLOUR_NAME)));

        var mediator = new Mock<IMediator>();

        ListProductsByColour qry = new ListProductsByColour(COLOUR_NAME);
        ListProductsByColourHandler handler = new ListProductsByColourHandler(mockRepository.Object);

        //Act
        IEnumerable<ProductListDto> result = await handler.Handle(qry, new System.Threading.CancellationToken());

        //Assert
        Assert.Single(result);

    }
}
