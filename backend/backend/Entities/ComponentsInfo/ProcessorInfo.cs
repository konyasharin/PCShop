namespace backend.Entities.ComponentsInfo
{
    public class ProcessorInfo
    {
        public int? ProductId { get; set; }
        public string Cores { get; set; }
        public string ClockFrequency { get; set; }
        public string TurboFrequency { get; set; }
        public string HeatDissipation { get; set; }
    }
}
