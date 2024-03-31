using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class MotherBoard<T> : Component<T>
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
    }
}
