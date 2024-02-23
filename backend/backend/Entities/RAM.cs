using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("RAM")]
    public class RAM
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

        [Column("frequency")]
        public required int Frequency { get; set; }

        [Column("timings")]
        public required int Timings { get; set; }

        [Column("capacity_db")]
        public required int Capacity_db { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }
    }
}
