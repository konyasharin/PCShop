namespace backend.Entities
{
    public class Order
    {
        public int? OrderId { get; set; }
        public string OrderStatus { get; set; } = String.Empty;
        public int UserId { get; set; }
    }
}