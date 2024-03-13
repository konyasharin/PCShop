using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedRam : RAM<IFormFile>
    {
        public bool updated { get; set; }
    }
}
