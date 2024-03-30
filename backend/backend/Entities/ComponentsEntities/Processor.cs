using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Processor<T> : Component<T>
    {
        public string Cores { get; set; }
        public string ClockFrequency { get; set; }
        public string TurboFrequency { get; set; }
        public string HeatDissipation { get; set; }
    }
}
