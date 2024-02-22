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
        public required string Brand { get; set; }

        [Column("model")]
        public required string Model { get; set; }

        [Column("price")]
        public required int Price { get; set; }

        [Column("country")]
        public required string Country { get; set; }

        [Column("material")]
        public required string Material { get; set; }

        [Column("width")]
        public required int Width { get; set; }

        [Column("height")]
        public required int Height { get; set; }

        [Column("depth")]
        public required int Depth { get; set; }

        [Column("image")]
        public required byte[] Image { get; set; }
    }
}
