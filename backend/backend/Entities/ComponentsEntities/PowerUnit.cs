using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class PowerUnit<T> : Product<T>
    {
      
        public string Battery { get; set; }
        public string Voltage { get; set; }
       

    }
}
