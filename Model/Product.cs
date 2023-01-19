using System;

namespace asp.net_core_web_api_learn.Model
{
    public class ProductVM
    {
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
    }

    public class Product : ProductVM
    {
        public Guid ProductId { get; set; }
        
    }
}