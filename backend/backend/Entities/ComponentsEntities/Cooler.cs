namespace backend.Entities
{
    public class Cooler<T> : Component<T>
    {
       
        public int Speed { get; set; }
        public int cooler_power { get; set; }
       
    }
}
