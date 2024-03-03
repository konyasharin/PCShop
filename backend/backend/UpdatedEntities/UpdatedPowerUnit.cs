using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedPowerUnit : PowerUnit<IFormFile>
    {
        public bool status_update { get; set; }
    }
}
