using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedComputerCase : ComputerCase<IFormFile>
    {
        public bool status_update { get; set; }
    }
}
