using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class VideoCard
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public int Memory_db { get; set; }
        public string Memory_type { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

       
    }
}
