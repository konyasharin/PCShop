using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedCooler : Cooler<IFormFile>
    {
        public bool status_update { get; set; }
    }
}
