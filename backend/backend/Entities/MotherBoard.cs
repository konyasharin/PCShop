using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("MotherBoard")]
    public class MotherBoard
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

        [Column("socket")]
        [Required]
        public string Socket { get; set; }

        [Column("chipset")]
        [Required]
        public string Chipset { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

        public Assembly assembly { get; set; }
    }
}
