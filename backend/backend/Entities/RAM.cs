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

        [Column("frequency")]
        [Required]
        public int Frequency { get; set; }

        [Column("timings")]
        [Required]
        public int Timings { get; set; }

        [Column("capacity_db")]
        [Required]
        public int Capacity_db { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

        public Assembly assembly { get; set; }
    }
}
