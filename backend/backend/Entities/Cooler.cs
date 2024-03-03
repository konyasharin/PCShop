using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Cooler<T> : Component<T>
    {
       
        public int Speed { get; set; }
        public int Power { get; set; }
       
    }
}
