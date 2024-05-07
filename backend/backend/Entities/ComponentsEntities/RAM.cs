using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class RAM<T> : Product<T>
    {
        public string Frequency { get; set; }
        public string Timings { get; set; }
        public string RamDb { get; set; }
    }
}
