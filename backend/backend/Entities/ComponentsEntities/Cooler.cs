namespace backend.Entities
{
    public class Cooler<T> : Component<T>
    {
        public string Speed { get; set; }
        public string CoolerPower { get; set; }
    }
}
