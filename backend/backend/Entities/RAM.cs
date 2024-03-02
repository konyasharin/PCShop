using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class RAM
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public int Frequency { get; set; }
        public int Timings { get; set; }
        public int Capacity_db { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

       
    }
}
