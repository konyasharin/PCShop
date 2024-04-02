using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class SSD<T> : Component<T>
    {
        public string Capacity { get; set; }
    }
}
