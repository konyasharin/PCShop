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
        [Required]
        public string Brand { get; set; }

        [Column("model")]
        [Required]
        public string Model { get; set; }

        [Column("price")]
        [Required]
        public int Price { get; set; }

        [Column("country")]
        [Required]
        public string Country { get; set; }

        [Column("memory_db")]
        [Required]
        public int Memoty_db { get; set; }

        [Column("memory_type")]
        [Required]
        public int Memory_type { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

        public Assembly assembly { get; set; }
    }
}
