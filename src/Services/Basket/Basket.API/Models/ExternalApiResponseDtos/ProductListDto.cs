﻿namespace Basket.API.Models.ExternalApiResponseDtos
{
    public class ProductListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
