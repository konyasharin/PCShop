using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("ComputerCase")]
    public class ComputerCase
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

        [Column("material")]
        [Required]
        public string Material { get; set; }

        [Column("width")]
        [Required]
        public int Width { get; set; }

        [Column("height")]
        [Required]
        public int Height { get; set; }

        [Column("depth")]
        [Required]
        public int Depth { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

        public Assembly assembly { get; set; }
       
    }
}
