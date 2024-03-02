using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Cooler
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public int Speed { get; set; }
        public int Power { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
