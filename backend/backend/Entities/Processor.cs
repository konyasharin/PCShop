using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("Processor")]
    public class Processor
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

        [Column("clock_frequency")]
        public required int Clock_frequency { get; set; }

        [Column("turbo_frequency")]
        public required int Turbo_frequency { get; set; }

        [Column("heat_dissipation")]
        public required int Heat_dissipation { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }
    }
}
