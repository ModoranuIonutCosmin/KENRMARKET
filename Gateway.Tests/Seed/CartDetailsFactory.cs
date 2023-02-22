using Gateway.Domain.Models.Carts;

namespace Gateway.Tests.Seed;

public class CartDetailsFactory
{
    private static string ProductId1 => "5DD9F8B0-22A5-4CC4-85FD-ABD0424DE45B";
    private static string ProductId2 => "29B204C5-C827-477C-BFE6-F2037742C745";

    private static string CustomerId1 => "49C60F75-3476-4AB4-BEAB-B7336E338329";
    private static string CustomerId2 => "811FD2F7-3784-41D4-81C8-3E350137339A";
    private static string CustomerId3 => "B7A159A0-1BFF-4C1D-B1FE-9B4D5AAD1375";
    
    //seed CartDetails objects
    public static List<CartDetails> SeedCartDetails()
    {
        return new List<CartDetails>()
        {
            new CartDetails()
            {
                CustomerId = new Guid(CustomerId1),
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        ProductId = Guid.Parse(ProductId1),
                        Quantity = 5
                    }
                }
            },
            new CartDetails()
            {
                CustomerId = new Guid(CustomerId2),
                CartItems = new List<CartItem>()
            },
            new CartDetails()
            {
                CustomerId = new Guid(CustomerId3),
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        ProductId = Guid.Parse(ProductId1),
                        Quantity = 5
                    },
                    new CartItem()
                    {
                        ProductId = Guid.Parse(ProductId2),
                        Quantity = 5
                    },
                }
            }
        };
    }




    //get customer with no items in cartItem helper
    public static CartDetails GetCartDetailsForCustomerWhichHasNoItemsInCart()
        => SeedCartDetails().First(c => !c.CartItems.Any());
    
    //get customer cart which has this product in cartItem helper
    public static CartDetails GetCartDetailsForCustomerWhichHasThisProductInCart(Guid productId)
        => SeedCartDetails().First(c => c.CartItems.Any(ci => ci.ProductId == productId));
    
    //get customer cart which does not have this product in cartItem helper and cart is not empty
    public static CartDetails GetCartDetailsForCustomerWhichDoesNotHaveThisProductInCart(Guid productId)
        => SeedCartDetails().First(c => c.CartItems.Any(ci => ci.ProductId != productId));
}