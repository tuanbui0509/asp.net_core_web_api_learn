using System;

namespace asp.net_core_web_api_learn.Models
{
    public class ProductVM
    {
        public string? ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public byte Discount { get; set; }

        public int? CategoryId { get; set; }
    }

    public class Product : ProductVM
    {
        public Guid ProductId { get; set; }
    }


    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
    }

}