﻿namespace Gateway.Domain.Models.Carts;

public class CartItemDTO
{
    public Guid    ProductId   { get; set; }
    public string  ProductName { get; set; }
    public decimal Quantity    { get; set; }
    public decimal UnitPrice   { get; set; }
    public string  PictureUrl  { get; set; }
}