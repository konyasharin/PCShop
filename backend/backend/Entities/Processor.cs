using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Processor
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int Cores { get; set; }
        public string Country { get; set; }
        public int Clock_frequency { get; set; }
        public int Turbo_frequency { get; set; }
        public int Heat_dissipation { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        
    }
}
