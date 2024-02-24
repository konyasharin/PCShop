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
        public DateTime creation_time { get; set; }

        [Column("processorId")]
        public long ProcessorId { get; set; }
        public Processor processor { get; set; }

        [ForeignKey("computerCaseId")]
        public long ComputerCaseId { get; set; }
        public ComputerCase computerCase { get; set; }

        [ForeignKey("coolerId")]
        public long CoolerId { get; set; }
        public Cooler cooler { get; set; }

        [ForeignKey("motherBoardId")]
        public long MotherBoardId { get; set; }
        public MotherBoard motherBoard { get; set; }

        [ForeignKey("powerUnitId")]
        public long PowerUnitId { get; set; }
        public PowerUnit powerUnit { get; set; }

        [ForeignKey("ramId")]
        public long RamId { get; set; }
        public RAM ram { get; set; }

        [ForeignKey("ramId")]
        public long SsdId { get; set; }
        public SSD ssd { get; set; }

        [ForeignKey("videoCardId")]
        public long VideoCardId { get; set; }
        public VideoCard videoCard { get; set; }

    }
}
