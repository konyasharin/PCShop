using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class RAM<T> : Component<T>
    {
        public string Frequency { get; set; }
        public string Timings { get; set; }
        public string RamDb { get; set; }
    }
}
