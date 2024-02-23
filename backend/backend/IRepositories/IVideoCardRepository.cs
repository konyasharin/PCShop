using backend.Entities;

namespace backend.IRepositories
{
    public interface IVideoCardRepository
    {
        Task<List<VideoCard>> GetAllVideoCards();
        Task<VideoCard> GetVideoCardById(long id);
        Task AddVideoCard(VideoCard videoCard);
        Task UpdateVideoCard(VideoCard videoCard);
        Task DeleteVideoCard(long id);
    }
}
