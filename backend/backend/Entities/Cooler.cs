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

        [Column("speed")]
        [Required]
        public int Speed { get; set; }

        [Column("power")]
        [Required]
        public int Power { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

       

    }
}
