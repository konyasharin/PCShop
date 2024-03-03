using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class PowerUnit<T> : Component<T>
    {
      
        public string Battery { get; set; }
        public int Voltage { get; set; }
       

    }
}
