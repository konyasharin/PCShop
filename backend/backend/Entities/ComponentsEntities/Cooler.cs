namespace backend.Entities
{
    public class Cooler<T> : Product<T>
    {
        public string Speed { get; set; }
        public string CoolerPower { get; set; }
    }
}
