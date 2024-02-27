using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using backend.Data;

namespace backend.Entities
{
    [Table("Assembly")]
    public class Assembly
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("likes")]
        public long Likes { get; set; }

        [Column("creation_time")]
        public DateTime Creation_time { get; set; }

        [Column("price")]
        [Required]
        public int Price { get; set; }

        [Column("processorId")]
        [ForeignKey("processorId")]
        public long ProcessorId { get; set; }
        public Processor processor { get; set; }

        [Column("computerCaseId")]
        [ForeignKey("computerCaseId")]
        public long ComputerCaseId { get; set; }
        public ComputerCase computerCase { get; set; }

        [Column("coolerId")]
        [ForeignKey("coolerId")]
        public long CoolerId { get; set; }
        public Cooler cooler { get; set; }

        [Column("motherBoardId")]
        [ForeignKey("motherBoardId")]
        public long MotherBoardId { get; set; }
        public MotherBoard motherBoard { get; set; }

        [Column("powerUnitId")]
        [ForeignKey("powerUnitId")]
        public long PowerUnitId { get; set; }
        public PowerUnit powerUnit { get; set; }

        [Column("ramId")]
        [ForeignKey("ramId")]
        public long RamId { get; set; }
        public RAM ram { get; set; }

        [Column("ssdId")]
        [ForeignKey("ssdId")]
        public long SsdId { get; set; }
        public SSD ssd { get; set; }

        [Column("videoCardId")]
        [ForeignKey("videoCardId")]
        public long VideoCardId { get; set; }
        public VideoCard videoCard { get; set; }

    }
}
