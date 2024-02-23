using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("SSD")]
    public class SSD
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
        public required int Capacity { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }
    }
}
