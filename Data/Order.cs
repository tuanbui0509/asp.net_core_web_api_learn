namespace asp.net_core_web_api_learn.Data
{
    public enum OrderStatus
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDay { get; set; }
        public DateTime? ShipDay { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? ReceivePerson { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}