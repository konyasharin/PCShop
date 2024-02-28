using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("like")]
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int LikeId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("assemblyid")]
        public int AssemblyId { get; set; }
    }
}
