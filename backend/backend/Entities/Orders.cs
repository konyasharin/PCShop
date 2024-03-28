namespace backend.Entities
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = String.Empty;
        public int UserId { get; set; }
    }
}