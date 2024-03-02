using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedMotherBoard : MotherBoard<IFormFile>
    {
        public bool status_update { get; set; }
    }
}
