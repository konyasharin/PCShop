namespace backend.Entities
{
    public class Order
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public string category { get; set; }
        public int quantity { get; set; }
        public int total_price { get; set; }
        public DateTime order_date { get; set; }

    }
}
