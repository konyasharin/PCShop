namespace backend.Entities
{
    public class Component<T>
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public T Image { get; set; }
    }
}
