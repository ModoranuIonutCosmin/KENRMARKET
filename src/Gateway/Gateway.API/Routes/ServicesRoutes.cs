namespace Gateway.API.Routes
{
    public static class ServicesRoutes
    {
        public static class Products
        {
            public static string AllProducts => "api/1.0/Products/all";
        }

        public static class Cart
        {
            public static string CartDetails => "api/1.0/Carts/cart";
        }

    }
}
