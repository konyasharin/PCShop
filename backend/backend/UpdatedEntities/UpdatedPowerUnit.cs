using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedPowerUnit : PowerUnit<IFormFile>
    {
        public bool updated { get; set; }
    }
}
