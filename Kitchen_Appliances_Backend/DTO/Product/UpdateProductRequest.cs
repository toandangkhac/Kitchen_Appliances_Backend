﻿namespace Kitchen_Appliances_Backend.DTO.Product
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        //public int CategoryId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
