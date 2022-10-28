namespace Gateway.API.Routes;

public static class ServicesRoutes
{
    public static class Products
    {
        public static string AllProducts => "api/1.0/Products/all";
        public static string FilteredProducts => "api/1.0/Products/filtered";

        public static string ProductById(Guid id)
        {
            return $"api/1.0/Products/{id}";
        }
    }

    public static class Cart
    {
        public static string CartDetails => "api/1.0/Carts/cart";
        public static string ModifyCartPut => "api/1.0/Carts/ModifyCart";
        public static string AddItemToCartPost => "api/1.0/Carts/AddItemToCart";
    }
}