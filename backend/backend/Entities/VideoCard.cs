using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("VideoCard")]
    public class VideoCard
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

        [Column("memory_db")]
        public required int Memoty_db { get; set; }

        [Column("memory_type")]
        public required int Memory_type { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }
    }
}
