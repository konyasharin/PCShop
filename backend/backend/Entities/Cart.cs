namespace backend.Entities
{
    public class Cart
    {
        public int? CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
