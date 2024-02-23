using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{

    [Table("Cooler")]

    public class Cooler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("brand")]
        public required string Brand { get; set; }

        [Column("model")]
        public required string Model { get; set; }

        [Column("price")]
        public required int Price { get; set; }

        [Column("country")]
        public required string Country { get; set; }

        [Column("speed")]
        public required int Speed { get; set; }

        [Column("power")]
        public required int Power { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }

    }
}
