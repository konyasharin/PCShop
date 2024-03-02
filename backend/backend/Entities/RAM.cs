using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class RAM<T> : Component<T>
    {
      
     
        public int Frequency { get; set; }
        public int Timings { get; set; }
        public int Capacity_db { get; set; }

       
    }
}
