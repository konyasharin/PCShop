using backend.Entities;

namespace backend.UpdatedEntities
{
    public class UpdatedVideoCard : VideoCard<IFormFile>
    {
        public bool status_update { get; set; }
    }
}
