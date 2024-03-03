using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedSsd : SSD<IFormFile>
    {
        public bool updated { get; set; }
    }
}
