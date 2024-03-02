using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Processor<T> : Component<T>
    {
     
        public int Cores { get; set; }
        public int Clock_frequency { get; set; }
        public int Turbo_frequency { get; set; }
        public int Heat_dissipation { get; set; }

        
    }
}
