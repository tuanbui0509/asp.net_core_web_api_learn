namespace asp.net_core_web_api_learn.Data
{
    public class OrderDetail
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public double Total { get; set; }
        public byte Discount { get; set; }

        // Relationship
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}