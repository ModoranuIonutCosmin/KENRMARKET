using System.Reflection;
using FakeItEasy;
using Gateway.Application.Interfaces;
using Gateway.Application.Profiles;
using Gateway.Application.Services;
using Gateway.Domain.Models.Carts;
using Gateway.Domain.Models.Products;
using Gateway.Tests.Seed;
using Testing.Utils.Mapper;

namespace Gateway.Tests.Services;

public class CartAggregatesServiceTests
{
    [Fact]
    public void Test_AddProductToCartWhenProductAlreadyInCartAndTotalSumSmallerThanTotalStock()
    {
        
        //GIVEN Cart aggregates service, a product and quantity to add and a customer id requesting the addition
        
        Product productToAdd = ProductsFactory.SeedProducts()[0];
        
        var productAndQuantityToAdd = new CartItemIdAndQuantity(
            ProductId: productToAdd.Id,
            Quantity: 1
        );

        var customerCartDetails = CartDetailsFactory
            .GetCartDetailsForCustomerWhichHasThisProductInCart(productAndQuantityToAdd.ProductId);
        
        
        var cartAggregatesService = SetupCartAggregatesServiceForAddToCart(customerCartDetails, productToAdd);
        
        //WHEN the product is to be added to the cart and the product exists in customer's cart
        
        var (isSuccess, cartDetails) = cartAggregatesService.AddItemToCart(Guid.NewGuid(),
            productAndQuantityToAdd).Result;
        
        //THEN the product is added to the cart and the cart details are returned
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;

        Assert.NotNull(cartDetailsAsStatus);
        Assert.True(isSuccess);
        Assert.Equal(6, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productToAdd.Id)
            .Sum(ci => ci.Quantity));
    }
    
    [Fact]
    public void Test_AddProductToCartWhenTotalQuantityExceeded()
    {
        //GIVEN Cart aggregates service, a product and quantity to add and a customer id requesting the addition
        
        Product productToAdd = ProductsFactory.SeedProducts()[0];
        
        var productAndQuantityToAdd = new CartItemIdAndQuantity(
            ProductId: productToAdd.Id,
            Quantity: 100
        );

        var customerCartDetails = CartDetailsFactory
            .GetCartDetailsForCustomerWhichHasThisProductInCart(productAndQuantityToAdd.ProductId);
        
        
        var cartAggregatesService = SetupCartAggregatesServiceForAddToCart(customerCartDetails, productToAdd);
        
        //WHEN the product is to be added to the cart and the total requested quantity exceeds stock
        
        var (isSuccess, cartDetails) = cartAggregatesService.AddItemToCart(Guid.NewGuid(),
            productAndQuantityToAdd).Result;
        
        //THEN the product is added to the cart and the cart details are returned
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;

        Assert.NotNull(cartDetailsAsStatus);
        Assert.NotEmpty(cartDetailsAsStatus.errors);
        Assert.True(isSuccess);
        Assert.Equal(10, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productToAdd.Id)
            .Sum(ci => ci.Quantity));
    }

    [Fact]
    public void Test_AddProductToCartWhenCartIsEmpty()
    {
        //GIVEN Cart aggregates service, a product and quantity to add and a customer id requesting the addition
        
        Product productToAdd = ProductsFactory.SeedProducts()[0];
        
        var productAndQuantityToAdd = new CartItemIdAndQuantity(
            ProductId: productToAdd.Id,
            Quantity: 1
        );

        var emptyCart = CartDetailsFactory.SeedCartDetails()[1];
        
        var cartAggregatesService = SetupCartAggregatesServiceForAddToCart(emptyCart, productToAdd);

        //WHEN the product is to be added to the cart and the cart is empty
        
        var (isSuccess, cartDetails) = cartAggregatesService.AddItemToCart(Guid.NewGuid(), productAndQuantityToAdd).Result;
        
        //THEN the product is added to the cart and the cart details are returned
        
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;
        
        Assert.NotNull(cartDetailsAsStatus);
        Assert.True(isSuccess);
        Assert.Equal(1, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productToAdd.Id)
            .Sum(ci => ci.Quantity));
        Assert.Single(cartDetailsAsStatus.cartDetails.CartItems);
    }

    [Fact]
    public void Test_ModifyCartWhenRequestedQuantityForAProductExceedsTotalStock()
    {
        // Given cart aggregates service, customerId and a cart update request with a product and quantity for a list of products
        
        var productsToAdd = ProductsFactory.SeedProducts();

        UpdateCartRequestDTO updateCartRequestDTO = new UpdateCartRequestDTO(
            CartItems: new List<CartItemIdAndQuantity>()
            {
                new CartItemIdAndQuantity(
                    ProductId: productsToAdd[0].Id,
                    Quantity: 100
                ),
                new CartItemIdAndQuantity(
                    ProductId: productsToAdd[1].Id,
                    Quantity: 100
                )
            },
            Promocode: "");
        
        var cartAggregatesService = SetupCartAggregatesServiceForModifyCart(CartDetailsFactory.SeedCartDetails()[0], productsToAdd);
        
        // When the cart quantity is to be modified but quantities requested exceed total stock
        
        var (isSuccess, cartDetails) = cartAggregatesService.ModifyCart(Guid.NewGuid(), updateCartRequestDTO).Result;
        
        // Then the cart details quantities for cart items is adjusted to maximum stock available and the cart details are returned
        // and errors about exceeding stock are added to the cart details
        
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;
        
        Assert.NotNull(cartDetailsAsStatus);
        Assert.NotEmpty(cartDetailsAsStatus.errors);
        Assert.True(isSuccess);
        Assert.Equal(10, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productsToAdd[0].Id)
            .Sum(ci => ci.Quantity));
        Assert.Equal(20, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productsToAdd[1].Id)
            .Sum(ci => ci.Quantity));
    }
    
    
    [Fact]
    public void Test_ModifyCartWhenCartDoesNotExist()
    {
        // Given cart aggregates service, customerId and a cart update request with a product and quantity for a list of products
        
        var productsToAdd = ProductsFactory.SeedProducts();

        UpdateCartRequestDTO updateCartRequestDTO = new UpdateCartRequestDTO(
            CartItems: new List<CartItemIdAndQuantity>()
            {
                new CartItemIdAndQuantity(
                    ProductId: productsToAdd[0].Id,
                    Quantity: 5
                ),
                new CartItemIdAndQuantity(
                    ProductId: productsToAdd[1].Id,
                    Quantity: 6
                )
            },
            Promocode: "");
        
        var cartAggregatesService = SetupCartAggregatesServiceForModifyCart(null, productsToAdd);
        
        // When the cart is to be created with updated quantities
        
        var (isSuccess, cartDetails) = cartAggregatesService.ModifyCart(Guid.NewGuid(), updateCartRequestDTO).Result;
        
        // Then the cart details quantities for cart items is adjusted and the cart details are returned
        
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;
        
        Assert.NotNull(cartDetailsAsStatus);
        Assert.True(isSuccess);
        Assert.Equal(5, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productsToAdd[0].Id)
            .Sum(ci => ci.Quantity));
        Assert.Equal(6, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productsToAdd[1].Id)
            .Sum(ci => ci.Quantity));
    }
    
    [Fact]
    public void Test_ModifyCartWhenOneOfTheProductsOfTheCartDoesNotExist()
    {
        // Given cart aggregates service, customerId and a cart update request with a product and quantity for a list of products
        
        var productsToAdd = ProductsFactory.SeedProducts();

        UpdateCartRequestDTO updateCartRequestDTO = new UpdateCartRequestDTO(
            CartItems: new List<CartItemIdAndQuantity>()
            {
                new CartItemIdAndQuantity(
                    ProductId: Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    Quantity: 50
                ),
                new CartItemIdAndQuantity(
                    ProductId: productsToAdd[1].Id,
                    Quantity: 6
                )
            },
            Promocode: "");
        
        var cartAggregatesService = SetupCartAggregatesServiceForModifyCart(CartDetailsFactory.SeedCartDetails()[0], productsToAdd);
        
        // When the cart quantities are to be updated
        
        var (isSuccess, cartDetails) = cartAggregatesService.ModifyCart(Guid.NewGuid(), updateCartRequestDTO).Result;
        
        // Then the cart details quantities for cart items is adjusted and the cart details are returned
        // and errors about non existing products are added to the cart details
        
        var cartDetailsAsStatus = cartDetails as CartUpdateStatusDto;
        
        Assert.NotNull(cartDetailsAsStatus);
        Assert.True(isSuccess);
        Assert.Contains(cartDetailsAsStatus.errors, e => e.cartActionErrorDetails is CartActionProductDoesntExistErrorDetailsDto);
        Assert.Equal(6, cartDetailsAsStatus.cartDetails.CartItems
            .Where(ci => ci.ProductId == productsToAdd[1].Id)
            .Sum(ci => ci.Quantity));
    }

    private static CartAggregatesService SetupCartAggregatesServiceForAddToCart(CartDetails customerCurrentCartDetails,
        Product productToAdd)
    {
        
        var cartServiceMock = A.Fake<ICartService>();

        A.CallTo(() => cartServiceMock.GetCartDetails(A<Guid>._))
            .Returns((true, emptyCart: customerCurrentCartDetails, null));

        A.CallTo(() => cartServiceMock.UpdateCart(A<Guid>._, A<CartDetails>._))
            .Returns(Task.FromResult<(bool, CartDetails, string)>((true, customerCurrentCartDetails, null)));

        var productsServiceMock = A.Fake<IProductsService>();

        A.CallTo(() => productsServiceMock.GetProductByIdAsync(A<Guid>._))
            .Returns((true, productToAdd, null));
        


        CartAggregatesService cartAggregatesService = new CartAggregatesService(productsServiceMock, cartServiceMock,
            AutoMapperHelpers.CreateMapper(new List<Assembly>() { typeof(CartDetailsToCartDetailsDTOProfile).Assembly }));
        return cartAggregatesService;
    }    
    
    private static CartAggregatesService SetupCartAggregatesServiceForModifyCart(CartDetails customerCurrentCartDetails,
        List<Product> allProducts)
    {
        
        var cartServiceMock = A.Fake<ICartService>();

        A.CallTo(() => cartServiceMock.GetCartDetails(A<Guid>._))
            .Returns((true, emptyCart: customerCurrentCartDetails, null));

        A.CallTo(() => cartServiceMock.UpdateCart(A<Guid>._, A<CartDetails>._))
            .Returns(Task.FromResult<(bool, CartDetails, string)>((true, customerCurrentCartDetails, null)));

        var productsServiceMock = A.Fake<IProductsService>();

        A.CallTo(() => productsServiceMock.GetProductsAsync())
            .Returns((true, allProducts, null));

        CartAggregatesService cartAggregatesService = new CartAggregatesService(productsServiceMock, cartServiceMock,
            AutoMapperHelpers.CreateMapper(new List<Assembly>() { typeof(CartDetailsToCartDetailsDTOProfile).Assembly }));
        return cartAggregatesService;
    }
}