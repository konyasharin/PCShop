using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class ComputerCase<T> : Component<T>
    {
        public string Material { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
    }
}
