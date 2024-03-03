using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class VideoCard<T> : Component<T>
    {
       
        public int Memory_db { get; set; }
        public string Memory_type { get; set; }
       
       
    }
}
