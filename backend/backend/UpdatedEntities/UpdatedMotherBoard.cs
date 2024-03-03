using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedMotherBoard : MotherBoard<IFormFile>
    {
        public bool updated { get; set; }
    }
}
