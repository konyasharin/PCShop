using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedProcessor : Processor<IFormFile>
    {
        public bool updated { get; set; }
    }
}
