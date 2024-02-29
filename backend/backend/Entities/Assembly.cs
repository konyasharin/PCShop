using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("Assembly")]
    public class Assembly
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        public int Price { get; set; }

        [Column("likes")]
        [Required]
        public int Likes { get; set; }

        [Column("creation_time")]
        [Required]
        public DateTime Creation_time { get; set; }

        [Column("computerCaseId")]
        public int ComputerCaseId { get; set; }

        [Column("coolerId")]
        public int CoolerId { get; set; }

        [Column("motherBoardId")]
        public int MotherBoardId { get; set; }

        [Column("powerUnitId")]
        public int PowerUnitId { get; set; }

        [Column("processorId")]
        public int ProcessorId { get; set; }

        [Column("ramId")]
        public int RamId { get; set; }

        [Column("ssdId")]
        public int SsdId { get; set; }

        [Column("videoCardId")]
        public int VideoCardId { get; set; }

    }
}
