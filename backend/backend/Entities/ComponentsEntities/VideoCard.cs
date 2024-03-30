using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class VideoCard<T> : Component<T>
    {
        public string MemoryDb { get; set; }
        public string MemoryType { get; set; }
    }
}
